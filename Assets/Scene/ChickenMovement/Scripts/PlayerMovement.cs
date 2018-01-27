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

		if (Input.touchCount > 0) {
			Movement ();
		}

	}

	void Movement () {

		Vector3 movement = new Vector3 (CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
		selfRigidbody.velocity = (-movement * speed);
		transform.rotation = Quaternion.LookRotation(-movement);

	}
}
