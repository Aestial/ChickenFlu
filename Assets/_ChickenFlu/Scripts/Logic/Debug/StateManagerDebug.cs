using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerDebug : MonoBehaviour 
{
    private Notifier notifier;
	// Use this for initialization
	void Awake () 
    {
        notifier = new Notifier();
        notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnEnter);
        notifier.Subscribe(StateManager.ON_STATE_EXIT, HandleOnExit);
	}
    void HandleOnEnter(params object[] args)
    {
        GameState state = (GameState)args[0];
        Debug.Log("STATE - Enter to state: " + state);
    }
    void HandleOnExit(params object[] args)
    {
        GameState state = (GameState)args[0];
        Debug.Log("STATE - Exit from state: " + state);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}