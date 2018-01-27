using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuletaController : MonoBehaviour {
    //public int num_players;
    public float force;
    public float speedBreak;

    public string []arrPlayers;
    public string[] reordenPlayers;
    private int selected;
   
    private float forceTurn;
    private bool isStarted = false;

    private float count;



    // Use this for initialization
    void Start()
    {
        //force = 0f;
        forceTurn = 0f;
        selected = 0;
        count = 3;
        for (int i = 0; i <= reordenPlayers.Length-1; i++)
        {
            reordenPlayers[i] = null;
        }

        for (int i = 0; i <= arrPlayers.Length-1; i++)
        {
            ReordenPlayers(arrPlayers[i]);
        }
    }

    // Update is called once per frame
    void Update() {
        
        if(count > 0 && !isStarted)
        {
            count = count-1 * Time.deltaTime;
            int c = (int)count;
            Debug.Log("count " + c);
        }
        else
        {
            if (!isStarted)
            {
                forceTurn += force;
                isStarted = true;
                Debug.Log("force " + forceTurn);
            }
        }
        
        
        if (isStarted) {
            if (forceTurn > 0f)
            {
                forceTurn -= speedBreak *Time.deltaTime;
                if (selected == reordenPlayers.Length - 1)
                {
                    selected = 0;
                }
                else {
                    selected++;
                }
                Debug.Log("jugador: " + reordenPlayers[selected]);
                //Debug.Log("force turn" + forceTurn);
            }
            else
            {
                Debug.Log("player selected: " + reordenPlayers[selected]);
            }
        }
	}

    void ReordenPlayers(string obj)
    {
        int temp;

        temp = Random.Range(0, arrPlayers.Length);
            if(reordenPlayers[temp] == null)
            {
                reordenPlayers[temp] = obj;
            Debug.Log("igualado" + temp + " i: " + obj);
            }
            else
            {
                ReordenPlayers(obj);
            }
        
    }

}
