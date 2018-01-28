﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public enum InputType 
{
    WASD,
    Arrows,
    TouchJoystick
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speedMultiplier = 4f;
    [SerializeField] private Animator animator;

    private Player player;
    private Rigidbody rb;
    private float speed;

	// Use this for initialization
	void Start () 
    {
        this.player = GetComponent<Player>();
		this.rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        this.speed = this.speedMultiplier * this.player.Speed;
        if (StateManager.Instance.State == GameState.Battle)
        {
            if (this.name == "Player0")
            {
                this.KeyboardMovement(InputType.WASD);
            }

            if (this.name == "Player1")
            {
                this.KeyboardMovement(InputType.Arrows);
            }

            if (this.name == "Player2")
            {
                this.TouchMovement();
            } 
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
	}

	void TouchMovement () 
    {
		if (Input.touchCount > 0) 
        {
			Vector3 movement = new Vector3 (CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
            this.rb.velocity = (movement * this.speed);
            if (movement != Vector3.zero)
            {
                this.transform.rotation = Quaternion.LookRotation(movement);    
            }
		}
	}

    void KeyboardMovement (InputType type) 
    {
        switch(type)
        {
            case InputType.Arrows:
                if (Input.GetAxis("KHorizontal") != 0 || Input.GetAxis("KVertical") != 0)
                {
                    Vector3 movement = new Vector3(Input.GetAxis("KHorizontal"), 0f, Input.GetAxis("KVertical"));
                    this.rb.velocity = (movement * this.speed);
                    this.transform.rotation = Quaternion.LookRotation(movement);
                }
                break;
            case InputType.WASD:
                if (Input.GetAxis("LKHorizontal") != 0 || Input.GetAxis("LKVertical") != 0)
                {
                    Vector3 movement = new Vector3(Input.GetAxis("LKHorizontal"), 0f, Input.GetAxis("LKVertical"));
                    this.rb.velocity = (movement * this.speed);
                    this.transform.rotation = Quaternion.LookRotation(movement);
                }
                break;
            default:
                break;
        }
	}
}