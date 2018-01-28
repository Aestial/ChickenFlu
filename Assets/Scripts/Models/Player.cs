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
};

[System.Serializable]
public struct SpeedState
{
    public PlayerState state;
    public float speed;
};

public class Player : MonoBehaviour 
{
    [SerializeField] private float infectedAmount;
    [SerializeField] private SpeedState[] speedStates;
    [SerializeField] private GraphicState[] graphicStates;

    [Header("Debug")]
    [SerializeField]
    private int number;
    [SerializeField]
    private float health;
    [SerializeField]
    private PlayerState state;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool canBeInfected;

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
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool CanBeInfected
    {
        get { return canBeInfected; }
        set { canBeInfected = value; }
    }

	void Start () 
    {
        this.health = 1.0f;
        this.state = PlayerState.Human;
        this.speed = speedStates[(int)this.state].speed;
        this.canBeInfected = true;
        // Temporary
		this.meshRenderer = GetComponent<MeshRenderer>();
        //
        // Notifier
        notifier = new Notifier();

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

    public void UpdateHealth(float amount)
    {
        this.health += amount;
        this.CheckHealth();
    }

    public void Mutate(PlayerState newState)
    {
        this.state = newState;
        this.meshRenderer.material = graphicStates[(int)this.state].material;
        this.speed = speedStates[(int)this.state].speed;
        //Debug.Log("Player " + this.number + "'s new state: " + this.state);
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
