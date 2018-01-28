using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioState
{
    public GameState state;
    public AudioClip clip;
};

public class GameAudioController : Singleton<GameAudioController> 
{
    [SerializeField] private AudioState[] audioStates;

    private Notifier notifier;
    // Use this for initialization
    void Awake()
    {
        notifier = new Notifier();
        notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnEnter);
        notifier.Subscribe(StateManager.ON_STATE_EXIT, HandleOnExit);
    }
    void HandleOnEnter(params object[] args)
    {
        GameState state = (GameState)args[0];
        PlayStateAudioLoop(state);
        Debug.Log("AUDIO - Playing loop of state: " + state);
    }
    void HandleOnExit(params object[] args)
    {
        GameState state = (GameState)args[0];
        AudioManager.Instance.StopLoop(state.ToString());
        Debug.Log("DEBUG - Exit from state: " + state);
    }
    private void PlayStateAudioLoop(GameState state)
    {
        foreach (AudioState aState in audioStates)
        {
            if (aState.state == state)
            {
                AudioManager.Instance.PlayLoop2D(state.ToString(), aState.clip);
            }
        }
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
