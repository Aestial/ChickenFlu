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

    void Awake()
    {
        notifier = new Notifier();
        notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnEnter);
        notifier.Subscribe(StateManager.ON_STATE_EXIT, HandleOnExit);
        this.PlayStateAudio(StateManager.Instance.State);
    }
    void HandleOnEnter(params object[] args)
    {
        GameState state = (GameState)args[0];
        this.PlayStateAudio(state);
        Debug.Log("AUDIO - Playing loop of state: " + state);
    }
    void HandleOnExit(params object[] args)
    {
        GameState state = (GameState)args[0];
        AudioManager.Instance.StopLoop(state.ToString());
    }
    private void PlayStateAudio(GameState state)
    {
        for (int i = 0; i < audioStates.Length; i++)
        {
            if (audioStates[i].state == state)
            {
                AudioManager.Instance.PlayLoop2D(state.ToString(), audioStates[i].clip);
            }
        }
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
