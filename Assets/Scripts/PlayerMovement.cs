using System.Collections;
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

	private Rigidbody selfRigidbody;
	public Vector3 currentJoystickInput;
	public float speed;

	// Use this for initialization
	void Start () {

		selfRigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (StateManager.Instance.State == GameState.Battle)
        {
            if (name == "Player0")
            {
                KeyboardMovement(InputType.WASD);
            }

            if (name == "Player1")
            {
                KeyboardMovement(InputType.Arrows);
            }

            if (name == "Player2")
            {
                TouchMovement();
            }   
        }		
	}

	void TouchMovement () 
    {
		if (Input.touchCount > 0) {
			Vector3 movement = new Vector3 (CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
			selfRigidbody.velocity = (movement * speed);
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movement);    
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
                    selfRigidbody.velocity = (movement * speed);
                    transform.rotation = Quaternion.LookRotation(movement);
                }
                break;
            case InputType.WASD:
                if (Input.GetAxis("LKHorizontal") != 0 || Input.GetAxis("LKVertical") != 0)
                {
                    Vector3 movement = new Vector3(Input.GetAxis("LKHorizontal"), 0f, Input.GetAxis("LKVertical"));
                    selfRigidbody.velocity = (movement * speed);
                    transform.rotation = Quaternion.LookRotation(movement);
                }
                break;
            default:
                break;

        }
	}

}
