using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplotion : MonoBehaviour {

	public Rigidbody rgbd;
	public float bulletForce;
	public float bulletRadius;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			rgbd.AddExplosionForce (bulletForce, transform.position, bulletRadius);
		}

	}
}
