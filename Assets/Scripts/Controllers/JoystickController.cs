using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour 
{
    [System.Serializable]
    public struct Joystick
    {
        public int number;
        public string name;
        public Player player;
    }

    private Joystick joystick;

    private const string LEFT_STICK_X = "LeftStickX-Player";
    private const string LEFT_STICK_Y = "LeftStickY-Player";

    public void Init(int num, string name)
    {
        this.joystick.number = num;
        this.joystick.name = name;
    }

    public void AssignPlayer(Player player)
    {
        this.joystick.player = player;
    }
	
	void Start () 
    {
        
	}
	
	void Update () 
    {
		
	}
}
