using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

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

		if (name == "Player1") {
			KeyboardMovement ();	
		}

		if (name == "Player2") {
			TouchMovement ();	
		}

	}

	void TouchMovement () {

		if (Input.touchCount > 0) {
			Vector3 movement = new Vector3 (CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
			selfRigidbody.velocity = (-movement * speed);
			transform.rotation = Quaternion.LookRotation(-movement);
		}

	}

	void KeyboardMovement () {

		if (Input.GetAxis ("KHorizontal") != 0 || Input.GetAxis ("KVertical") != 0) {
			Vector3 movement = new Vector3 (Input.GetAxis("KHorizontal"), 0f, Input.GetAxis("KVertical"));
			selfRigidbody.velocity = (-movement * speed);
			transform.rotation = Quaternion.LookRotation(-movement);	
		}

	}
}
