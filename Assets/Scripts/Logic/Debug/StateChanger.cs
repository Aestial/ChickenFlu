using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour 
{
    void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Plus))
        {
            StateManager.Instance.State++;
        } 
        else if (Input.GetKeyUp(KeyCode.Minus))
        {
            StateManager.Instance.State--;

        }
        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //{
        //    StateManager.Instance.State = GameState.Start;
        //}
        //else if (Input.GetKeyUp(KeyCode.Alpha2))
        //{ 
        //    StateManager.Instance.State = GameState.Battle;
        //}
        //else if (Input.GetKeyUp(KeyCode.Alpha3))
        //{
        //    StateManager.Instance.State = GameState.End;
        //}
    }
}
