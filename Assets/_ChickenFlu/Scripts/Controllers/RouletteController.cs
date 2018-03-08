using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : Singleton<RouletteController> {

    private Notifier notifier;
    public const string ON_UPDATE_SELECTED = "OnUpdateSelected";
    public const string ON_FINISH_SELECTED = "OnSelectedInfected";

    [SerializeField] private float finishTime;

    private int numPlayers;
    private int currentPlayer;
    private int playerSelected;
    private int lastCurrent;

    void Start () {
        notifier = new Notifier();
        lastCurrent = 0;
        currentPlayer = 0;
    }

    public void Initialize(int playersCount) 
    {
        numPlayers = playersCount;
        if (numPlayers != 0)
        {
            // TimerControl(240f, 2,0f);
            //TimerControl(250f, 2, 480f);
            TimerControl(250f, 2, 730f);
            TimerControl(260.86f, 2, 980f );
            TimerControl(272.72f, 2, 1501.72f);
            TimerControl(285.7f, 2, 2047.16f);
            TimerControl(300f, 2, 2618.56f);
            TimerControl(315.78f, 2, 3218.56f);
            TimerControl(333.32f, 2, 3850.12f);
            TimerControl(352.94f, 2, 4516.76f);
            TimerControl(375f, 2, 5222.64f);
            TimerControl(400f, 2, 5972.64f);
            TimerControl(428.56f, 2, 6772.64f);
            TimerControl(461.52f, 2, 7629.76f);
            TimerControl(500f, 2, 8552.8f);
            TimerControl(500f, 2, 9552.8f);
            Invoke("Selected", 10f);
			Debug.Log ("Initialize");
        }
        else
        {
            Debug.LogError("Players don´t exist");
        }
    }
	

    void ChangeSelected()
    {
        currentPlayer = Random.Range(0, numPlayers);
		Debug.Log ("Change selected: currentPlyer=" + currentPlayer);
        if(currentPlayer == lastCurrent)
        {
            ChangeSelected();
        }
        else
        {
            lastCurrent = currentPlayer;
            notifier.Notify(ON_UPDATE_SELECTED, currentPlayer);
        }
    }

    void Selected()
    {
        currentPlayer = Random.Range(0, numPlayers);
		Debug.Log ("Selected: currentPlayer=" + currentPlayer);
        if (currentPlayer == lastCurrent)
        {
            Selected();
        }
        else
        {
            Debug.Log("Roulette - First Player infected: " + currentPlayer);
            playerSelected = currentPlayer;
            StartCoroutine(OnSelected(finishTime));
        }
    }

    void TimerControl(float time, int repeat,float delay)
    {
        
        time *= 0.001f;
        delay *= 0.001f;
        for(int i =1; i <= repeat; i++)
        {
            Invoke("ChangeSelected", (time * i)+delay);   
        }
    }

    private IEnumerator OnSelected(float wait)
    {
        yield return new WaitForSeconds(wait);
        notifier.Notify(ON_FINISH_SELECTED, playerSelected);
		Debug.Log ("OnSelected");
    }

    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }

}
