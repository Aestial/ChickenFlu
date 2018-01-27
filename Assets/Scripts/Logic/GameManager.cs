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

	void Start () 
    {
        this.players = new Player[this.numPlayers];
        for (int i = 0; i < this.numPlayers; i++) 
        {
            this.players[i] = Instantiate<Player>(this.playerPrefab, positions[i], Quaternion.identity);
            this.players[i].Number = i;
        }
        StartCoroutine(this.Roulette());
	}

    private IEnumerator Roulette () 
    {
        yield return new WaitForSeconds(1.8f);
        this.infected = Random.Range(0, this.numPlayers);
        Debug.Log(this.infected);
        this.players[this.infected].Mutate(PlayerState.Infected);
    }
	
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            this.Infect(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        { 
            this.Infect(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            this.Infect(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            this.Infect(3);
        }
	}
    private void Infect(int player)
    {
        this.players[this.infected].Mutate(PlayerState.Human);
        this.infected = player;
        Debug.Log(this.infected);
        this.players[this.infected].Mutate(PlayerState.Infected);
    }
}
