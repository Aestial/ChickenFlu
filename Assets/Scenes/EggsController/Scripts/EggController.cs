using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour {

	public GameObject eggPrefab;
	public int ammoLimit;
	public Transform eggSource;
	public int forceMultiply;
	private int eggIndex;
	private GameObject[] eggAmmo;

	// Use this for initialization
	void Start () {

		eggAmmo = new GameObject[ammoLimit];
		eggIndex = 0;

		for (int i = 0; i < ammoLimit; i++) {
			eggAmmo[i] = Instantiate(eggPrefab, eggSource.position, Quaternion.identity); 
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space)) {

			Vector3 fwd = transform.TransformDirection(Vector3.forward);

			if (eggIndex < ammoLimit - 1) {
				eggIndex++;
			} else {
				eggIndex = 0;
			}

			//eggAmmo[eggIndex].GetComponent<Rigidbody>().AddForce(fwd);
			eggAmmo[eggIndex].GetComponent<Rigidbody>().AddForceAtPosition(fwd * forceMultiply);

		}

	}

}
