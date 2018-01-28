using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour 
{
    [SerializeField] private Light[] directionals;
    [SerializeField] private Light[] spots;
	
    private Notifier notifier;

    void Awake()
    {
        notifier = new Notifier();
        notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnEnter);
        notifier.Subscribe(StateManager.ON_STATE_EXIT, HandleOnExit);
        notifier.Subscribe(RouletteController.ON_UPDATE_SELECTED, HandleOnUpdateRoulette);
        notifier.Subscribe(RouletteController.ON_FINISH_SELECTED, HandleOnUpdateRoulette);
        this.SetLighting(StateManager.Instance.State);
    }
    void HandleOnEnter(params object[] args)
    {
        GameState state = (GameState)args[0];
        this.SetLighting(state);
        Debug.Log("LIGHTS - Setting lighting for state: " + state);
    }
    void HandleOnExit(params object[] args)
    {
        GameState state = (GameState)args[0];
    }
    void HandleOnUpdateRoulette(params object[] args)
    {
        int index = (int)args[0];
        SetLightAtIndex(spots, index);
    }
    private void SetLighting(GameState state)
    {
        switch(state)
        {
            case GameState.Roulette:
                this.SwitchLights(directionals, false);
                break;
            case GameState.Battle:
                this.SwitchLights(directionals, true);
                break;
            default:
                this.SwitchLights(spots, false);
                break;
        }
    }
    private void SwitchLights(Light[] lights, bool on)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = on;
        }
    }
    private void SetLightAtIndex(Light[] lights, int index)
    {
        SwitchLights(lights, false);
        lights[index].enabled = true;
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
