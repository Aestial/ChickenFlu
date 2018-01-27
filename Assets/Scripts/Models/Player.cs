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

    private SpriteRenderer sr;

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

	void Start () 
    {
        // Temporary
        this.sr = GetComponent<SpriteRenderer>();
        //
        this.state = PlayerState.Human;
        this.health = 1.0f;
	}

    private void UpdateHealth (float amount)
    {
        this.health += amount;
        Debug.Log(this.health);
        this.CheckHealth();
    }

    private void CheckHealth()
    {
        if (this.health <= 0.0f) 
        {
            Debug.Log("Player " + number + " is dead!");
            this.Mutate(PlayerState.MadChicken);
        }
    }
    public void Mutate(PlayerState newState)
    {
        this.state = newState;
        this.sr.color = GStates[(int)this.state].color;
        Debug.Log("Player " + this.number + "'s new state: " + this.state);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.state == PlayerState.Infected) 
        {
            this.UpdateHealth(-infectedAmount);
        }
	}
}
