using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluTransmission : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision currentCollision) {



		Debug.Log (currentCollision.transform.name);

	}

	public void Infected() {



	}

}
