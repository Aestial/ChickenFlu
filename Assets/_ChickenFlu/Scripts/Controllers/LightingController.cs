using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour 
{
    [SerializeField] private Light[] directionals;
    [SerializeField] private Light spot;
    [SerializeField] private Transform spawnPositions;
    [SerializeField] private Vector3 spotSpawnOffset;
	
    private Notifier notifier;

    void Start() {
        
    }

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
        SetLightAtIndex(spot, index);
    }
    private void SetLighting(GameState state)
    {
        switch(state)
        {
            case GameState.Roulette:
                this.SwitchLights(directionals, false);
                this.SwitchLight(spot, true);
                break;
            case GameState.Battle:
                this.SwitchLights(directionals, true);
                this.SwitchLight(spot, false);
                break;
            default:
                this.SwitchLight(spot, false);
                break;
        }
    }
    private void SwitchLight(Light light, bool on)
    {
        light.enabled = on;
    }
    private void SwitchLights(Light[] lights, bool on)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            SwitchLight(lights[i], on);
        }
    }
    private void SetLightAtIndex(Light light, int index)
    {
        Vector3 pos = spawnPositions.GetChild(index).position;
        pos += spotSpawnOffset;
        SetLightPosition(light, pos);
    }

    private void SetLightPosition(Light light, Vector3 pos)
    {
        light.transform.position = pos;
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
