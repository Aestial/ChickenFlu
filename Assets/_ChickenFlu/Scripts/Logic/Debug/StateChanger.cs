using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour 
{
    private int length = Enum.GetNames(typeof(GameState)).Length;

    // private GameState last = Enum.GetValues(typeof(GameState)).Cast<GameState>().Last();
    // private GameState first = Enum.GetValues(typeof(GameState)).Cast<GameState>().Last();

    void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Plus))
        {
            StateManager.Instance.State++;
            if ( (int) StateManager.Instance.State >= length )
                StateManager.Instance.State = (GameState)0;

        } 
        else if (Input.GetKeyUp(KeyCode.Minus))
        {
            StateManager.Instance.State--;
            if ( StateManager.Instance.State < 0 )
                StateManager.Instance.State = (GameState)length - 1;
        }
    }
}
