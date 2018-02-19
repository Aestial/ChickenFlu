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
			eggAmmo[i].SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space) && (eggIndex < ammoLimit)) {

			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			//eggAmmo[eggIndex].GetComponent<Rigidbody>().AddForce(fwd);
			eggAmmo[eggIndex].SetActive(true);
			eggAmmo[eggIndex].transform.position = eggSource.transform.position;
			Rigidbody eggRgbd = eggAmmo[eggIndex].GetComponent<Rigidbody>();
			eggRgbd.AddForceAtPosition(fwd * forceMultiply, eggSource.position, ForceMode.Force);
			eggRgbd.useGravity = true;
			eggAmmo[eggIndex].GetComponent<MeshCollider>().isTrigger = false;
			eggIndex++; 

		}

	}

}
