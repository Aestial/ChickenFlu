using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour {

	private Rigidbody selfRigidbody;
	public Vector3 currentJoystickInput;
	public float speed;

	// Use this for initialization
	void Start () {

		selfRigidbody = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetAxis("KHorizontal") != 0 || Input.GetAxis("KVertical") != 0) {
			Movement ();
		}

	}

	void Movement () {

		Vector3 movement = new Vector3 (Input.GetAxis("KHorizontal"), 0f, Input.GetAxis("KVertical"));
		selfRigidbody.velocity = (-movement * speed);
		transform.rotation = Quaternion.LookRotation(-movement);

	}

}
