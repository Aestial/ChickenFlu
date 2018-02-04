using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Notifier notifier;
    private new Camera camera;

    private Vector3 position;
    private Quaternion rotation;
    private float fieldOfView;


    void Awake()
    {
        notifier = new Notifier();
        notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnEnter);
        notifier.Subscribe(StateManager.ON_STATE_EXIT, HandleOnExit);
    }
    void Start()
    {
        this.camera = GetComponent<Camera>();
        this.position = this.transform.position;
        this.rotation = this.transform.rotation;
        this.fieldOfView = this.camera.fieldOfView;
    }
    void HandleOnEnter(params object[] args)
    {
        GameState state = (GameState)args[0];
        this.PlayStateAnim(state);
        Debug.Log("AUDIO - Playing loop of state: " + state);
    }
    void HandleOnExit(params object[] args)
    {
        GameState state = (GameState)args[0];
        AudioManager.Instance.StopLoop(state.ToString());
    }
    private void PlayStateAnim(GameState state)
    {
        if (state == GameState.Winner)
        {
            StartCoroutine(SetTarget(0.0f));
            StartCoroutine(SetTarget(0.8f));
        }
    }
    private IEnumerator SetTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        Transform target = GameManager.Instance.Winner.transform.GetChild(0);
        this.transform.position = target.position;
        this.transform.rotation = target.rotation;
        this.camera.fieldOfView = this.fieldOfView - 8.0f;
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
