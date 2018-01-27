using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    [SerializeField] private int numPlayers;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Vector3[] positions;

    [Header("Debug")]
    [SerializeField] 
    private Player[] players;
    [SerializeField]
    private int infected;
    [SerializeField]
    private int remain;

    private RouletteController roulette;
    private Notifier notifier;

	void Start () 
    {
        this.remain = this.numPlayers;

        this.players = new Player[this.numPlayers];
        for (int i = 0; i < this.numPlayers; i++) 
        {
            this.players[i] = Instantiate<Player>(this.playerPrefab, this.positions[i], Quaternion.identity);
			this.players [i].name = "Player" + (i+1).ToString();
            this.players[i].Number = i;
        }
        this.roulette = GetComponent<RouletteController>();

        // Notifier
        notifier = new Notifier();
        notifier.Subscribe(Player.ON_DIE, HandleOnDie);
        notifier.Subscribe(RouletteController.ON_SELECTED_INFECTED, HandleOnSelectedInfected);

        StartCoroutine(this.Roulette());
	}

    

    private IEnumerator Roulette()
    {
        yield return new WaitForSeconds(1.0f);
        StateManager.Instance.State = GameState.Roulette;
        this.roulette.Initialize(numPlayers);
        //this.infected = Random.Range(0, this.numPlayers);
        //Debug.Log(this.infected);
        this.players[this.infected].Mutate(PlayerState.Infected);
        StateManager.Instance.State = GameState.Battle;
    }
    	
	void Update () 
    {
        if (StateManager.Instance.State == GameState.Battle)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.Infect(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.Infect(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.Infect(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                this.Infect(3);
            }
        }
	}
 
    private void Infect(int player)
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
        Debug.Log("manager infected" + infected);
    }

    private void UpdateRemain()
    {
        remain--;
        if (remain <= 1)
        {
            StateManager.Instance.State = GameState.End;
            this.players[this.infected].Mutate(PlayerState.Chicken);
        }
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }

}
