using UnityEngine;
public enum GameState
{
    /** Game start */
    Start,
    /** Random select */
    Roulette,
    /** Battle loop */
    Battle,
    /** Two Remaining */
    StressBattle,
    /** Winner focus */
    Winner,
    /** Game end */
    End
}
public class StateManager : Singleton<StateManager>
{
    private GameState state;

    private Notifier notifier;
	public const string ON_STATE_ENTER = "OnStateEnter";
    public const string ON_STATE_EXIT = "OnStateExit";

    public GameState State
    {
        get { return state; }
        set { SetState(value); }
    }

    void Start()
    {
        notifier = new Notifier();
        state = GameState.Start;
    }
    private void SetState(GameState newState)
    {
        OnExit(state);
        state = newState;
        OnEnter(state);
    }
    private void OnEnter(GameState s)
    {
		notifier.Notify(ON_STATE_ENTER, s);
    }
	private void OnExit(GameState s)
	{
		notifier.Notify(ON_STATE_EXIT, s);
	}
	void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
