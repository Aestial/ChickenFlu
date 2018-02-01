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
    }
}
