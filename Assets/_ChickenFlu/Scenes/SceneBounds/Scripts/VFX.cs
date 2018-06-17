using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {

	public GameObject pSystem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiatePSystem() {

		GameObject newParticleSystem = Instantiate (pSystem, transform.position, Quaternion.identity);
		newParticleSystem.transform.SetParent (transform);
	}
}
