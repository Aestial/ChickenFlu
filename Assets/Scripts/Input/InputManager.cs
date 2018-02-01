using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> 
{
    public int numJoysticks;
    private string[] joystics;
	// Use this for initialization
	void Start () 
    {
        this.joystics = Input.GetJoystickNames();
        this.numJoysticks = this.joystics.Length;
        Debug.Log("INPUT MANGER - Joysticks connected: " + this.numJoysticks);
        DebugJoysticks();
	}
	void DebugJoysticks()
    {
        for (int i = 0; i < this.joystics.Length; i++)
        {
            Debug.Log("INPUT MANGER - Joystick " + i + ": " + joystics[i]);
        }
    }
	// Update is called once per frame
	void Update () 
    {
		
	}
}
