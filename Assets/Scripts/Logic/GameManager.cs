using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
    [SerializeField] private int numPlayers;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform spawnPositions;
    // TODO: Temporary 
    [SerializeField] private float winnerTime;
    //
    [SerializeField] private Transform healthPanel;
    [SerializeField] private Texture[] playerTextures;

    [Header("Debug")]
    [SerializeField] 
    private Player[] players;
    [SerializeField]
    private int infected;
    [SerializeField]
    private int remain;
    [SerializeField]
    private bool started;
    private RouletteController roulette;
    private Notifier notifier;

	void Start () 
    {
        this.remain = this.numPlayers;
        this.players = new Player[this.numPlayers];
        for (int i = 0; i < this.numPlayers; i++) 
        {
            Vector3 position = spawnPositions.GetChild(i).position;
            this.players[i] = Instantiate<Player>(this.playerPrefab, position, Quaternion.identity);
			this.players[i].name = "Player" + (i).ToString();
            this.players[i].Number = i;
            this.players[i].Texture = playerTextures[i];
            this.players[i].UI = healthPanel.GetChild(i).GetComponent<PlayerUIController>();
            Vector3 lookTarget = new Vector3(0, this.players[i].transform.position.y, 0);
            this.players[i].transform.LookAt(lookTarget);
        }
        this.started = false;
        this.roulette = GetComponent<RouletteController>();

        // Notifier
        notifier = new Notifier();
        notifier.Subscribe(Player.ON_DIE, HandleOnDie);
        notifier.Subscribe(RouletteController.ON_FINISH_SELECTED, HandleOnSelectedInfected);

	}
    private void Update()
    {
        if( StateManager.Instance.State == GameState.Start &&
            Input.GetKeyUp(KeyCode.Return) && !started)
        {
            started = true;
            StartCoroutine(this.SpinRoulette());
        }
        if ( StateManager.Instance.State == GameState.End &&
            Input.GetKeyUp(KeyCode.Return))
        {
            // TODO: Change This!
            SceneManager.LoadScene("Main");
        }
    }

    public void Infect(int player)
    {
        if (this.infected != player &&
            this.players[player].State == PlayerState.Human)
        {
            this.players[player].Mutate(PlayerState.Infected);

            if (this.players[this.infected].State == PlayerState.Infected)
            {
                this.players[this.infected].Mutate(PlayerState.Human);
            }
            else if (this.players[this.infected].State == PlayerState.MadChicken)
            {
                this.players[this.infected].Mutate(PlayerState.Chicken);
            }
            this.infected = player;
        }
    }

    private void HandleOnDie(params object[] args)
    {
        UpdateRemain();
    }

    private void HandleOnSelectedInfected(object[] args)
    {
        this.infected = (int)args[0];
        //Debug.Log("Manager - Infected: " + infected);
        this.players[this.infected].Mutate(PlayerState.Infected);
        StateManager.Instance.State = GameState.Battle;
    }

    private void UpdateRemain()
    {
        remain--;
        if (remain <= 1)
        {
            StateManager.Instance.State = GameState.Winner;
            StartCoroutine(this.WinnerWait(this.winnerTime));
            // TODO: Not working
            this.players[this.infected].Mutate(PlayerState.Chicken);
        }
    }

    private IEnumerator SpinRoulette()
    {
        yield return new WaitForSeconds(0.5f);
        StateManager.Instance.State = GameState.Roulette;
        this.roulette.Initialize(this.numPlayers);
    }

    private IEnumerator WinnerWait(float time)
    {
        yield return new WaitForSeconds(time);
        StateManager.Instance.State = GameState.End;
    }

    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }

}
