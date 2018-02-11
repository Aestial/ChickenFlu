using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    WASD,
    Arrows,
    Joystick0,
    Joystick1,
    Joystick2,
    Joystick3
}

public class InputManager : Singleton<InputManager> 
{
    public bool checkInUpdate;
    public int numJoysticks;
    [SerializeField] private JoystickController joystickPrefab;
    [SerializeField] private RectTransform m_UIParent;
    private JoystickController[] joysticks;
    private string[] joystickNames;
	// Use this for initialization
	void Start () 
    {
        
        this.joystickNames = Input.GetJoystickNames();
        this.numJoysticks = this.joystickNames.Length;
        Debug.Log("INPUT MANGER - Joysticks connected: " + this.numJoysticks);
        DebugJoysticks();
        CreateJoysticks();
	}

    void CreateJoysticks() 
    {
        for (int i = 0; i < this.numJoysticks; i++)
        {
            Instantiate<JoystickController>(joystickPrefab,m_UIParent);
        }    
    }

	void DebugJoysticks()
    {
        for (int i = 0; i < this.numJoysticks; i++)
        {
            Debug.Log("INPUT MANGER - Joystick " + i + ": " + this.joystickNames[i]);
        }
    }

    void Update () 
    {
        if (checkInUpdate)
        {
            this.joystickNames = Input.GetJoystickNames();
            int newNum = this.joystickNames.Length;
            this.UpdateInput(newNum);
        }
	}

    private void UpdateInput(int num)
    {
        if (num != this.numJoysticks)
        {
            if(this.numJoysticks < num)
            {
                Debug.Log("New Joystick Detected");
            }
            else 
            {
                Debug.Log("Joystick Disconnected");
            }
            this.numJoysticks = num;
        }
    }
}
