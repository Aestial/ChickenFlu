using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    /** Initial */
    Human,
    /** Infected */
    Infected,
    /** Dying */
    MadChicken,
    /** Dead */
    Chicken
}

[System.Serializable]
public struct GraphicState 
{
    public PlayerState state;
	public Material material;
}

public class Player : MonoBehaviour 
{
    [SerializeField] private float infectedAmount;
    [SerializeField] private GraphicState[] GStates;

    [Header("Debug")]
    [SerializeField]
    private int number;
    [SerializeField]
    private float health;
    [SerializeField]
    private PlayerState state;

	private MeshRenderer meshRenderer;

    private Notifier notifier;
    public const string ON_DIE = "OnDie";

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

    public PlayerState State
    {
        get { return state; }
        set { state = value; }
    }

	void Start () 
    {
        this.state = PlayerState.Human;
        this.health = 1.0f;
        // Temporary
		this.meshRenderer = GetComponent<MeshRenderer>();
        //
        // Notifier
        notifier = new Notifier();

	}

    private void UpdateHealth (float amount)
    {
        this.health += amount;
        this.CheckHealth();
    }

    private void CheckHealth()
    {
        if (this.health <= 0.0f) 
        {
            Debug.Log("Player " + number + " is dead!");
            notifier.Notify(ON_DIE);
            this.Mutate(PlayerState.MadChicken);
        }
    }

    public void Mutate(PlayerState newState)
    {
        this.state = newState;
		this.meshRenderer.material = GStates[(int)this.state].material;
        Debug.Log("Player " + this.number + "'s new state: " + this.state);
    }

    void Update () 
    {
        if (this.state == PlayerState.Infected) 
        {
            this.UpdateHealth(-infectedAmount);
        }
	}
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
