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
    public Color color;
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

    [SerializeField] private SkinnedMeshRenderer smr;

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
    [SerializeField] 
    private Material material;
    [SerializeField]
    private PlayerUIController ui;

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
    public PlayerUIController UI
    {
        get { return ui; }
        set { ui = value; }
    }

	void Start () 
    {
        this.health = 1.0f;
        this.state = PlayerState.Human;
        this.speed = speedStates[(int)this.state].speed;
        this.material = smr.material;
        this.material.color = graphicStates[(int)this.state].color;
        this.canBeInfected = true;
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
        this.ui.UpdateHealth(this.health);
    }

    public void Mutate(PlayerState newState)
    {
        this.state = newState;
        this.speed = speedStates[(int)this.state].speed;
        this.material.color = graphicStates[(int)this.state].color;
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
