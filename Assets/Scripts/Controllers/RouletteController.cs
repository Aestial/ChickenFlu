using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : Singleton<RouletteController> {

    private Notifier notifier;
    public const string ON_SELECTED_INFECTED = "OnSelectedInfected";

    //public Sprite imagePlayer;
    //public AudioClip audio;

    //private AudioSource audioSource;

    public int numPlayers;
    //private Image[] playersCard;
    //private RuletaController ruleta;

    private int currentPlayer;
    private int playerSelected;
    private int lastCurrent;
    

    // Use this for initialization
    void Start () {
        notifier = new Notifier();
        //audioSource = GetComponent<AudioSource>();
        lastCurrent = 0;
        currentPlayer = 0;

        //playersCard = new Image[numPlayers];
        //for (int i =0; i < numPlayers; i++)
        //{
        //    playersCard[i] = transform.GetChild(i).GetComponent<Image>();
        //    playersCard[i].color = Color.gray;
        //    playersCard[i].sprite = imagePlayer;
        //}
    }

    public void Initialize(int numPlayers) 
    {

        if (numPlayers != 0)
        {
            TimerControl(240f, 2, 0f);
            TimerControl(250f, 2, 480f);
            TimerControl(260.86f, 2, 980f);
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
            Invoke("Selected", 10f);
        }
        else
        {
            Debug.Log("Players don´t exist");
        }
    }
	

    void ChangeSelected()
    {
        //Debug.Log(numPlayers);
        currentPlayer = Random.Range(0, numPlayers);
        if(currentPlayer == lastCurrent)
        {
            ChangeSelected();
        }
        else
        {
            //playersCard[currentPlayer].color = Color.white;
            //playersCard[lastCurrent].color = Color.gray;
            lastCurrent = currentPlayer;
        }

        /*if(!audioSource.isPlaying)
        {
            
            audioSource.Play();
        }*/
    }

    void Selected()
    {
        currentPlayer = Random.Range(0, numPlayers);
        if (currentPlayer == lastCurrent)
        {
            Selected();
        }
        else
        {
            //playersCard[currentPlayer].color = Color.red;
            //playersCard[lastCurrent].color = Color.gray;
            Debug.Log("First Player infected: " + currentPlayer);
            OnSelected(currentPlayer); 
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

    private void OnSelected(int select)
    {
        playerSelected = select;
        notifier.Notify(ON_SELECTED_INFECTED, playerSelected);
    }


    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }

}
