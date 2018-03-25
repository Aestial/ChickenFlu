using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZMovement : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	private Vector3 rotateAxy = new Vector3(0, 1, 0);
	private Vector3 forward = new Vector3(0, 0, 1);

	// Use this for initialization
	void Start () {
		if (this.name != "Player0") {
			this.GetComponent<EZMovement>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate(forward * speed);
		} else if (Input.GetKey(KeyCode.S)) {
			transform.Translate(forward * -speed);
		}

		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(rotateAxy * rotateSpeed);
		} else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(rotateAxy * -rotateSpeed);
		}

	}
}
