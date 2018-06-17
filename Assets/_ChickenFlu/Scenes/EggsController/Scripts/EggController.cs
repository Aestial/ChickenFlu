using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour {

	private Rigidbody playerRdbd;
	public GameObject eggPrefab;
	public int ammoLimit;
	public Transform eggSource;
	public int explosionForce;
	public int explosionRadius;
	public int upwardsModifier;
	public int forceMultiply;
	private int eggIndex;
	private GameObject[] eggAmmo;
	private AudioSource audioSource;
	public AudioClip audioClip;

	// Use this for initialization
	void Start () {

		playerRdbd = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		eggAmmo = new GameObject[ammoLimit];
		eggIndex = 0;

		for (int i = 0; i < ammoLimit; i++) {
			eggAmmo[i] = Instantiate(eggPrefab, eggSource.position, Quaternion.identity);
			eggAmmo [i].GetComponent<EggBehaviour2>().eggIdentifier = gameObject.GetComponent<Player>().Id;
			eggAmmo[i].SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		/*if (this.name == "Player0") {

			if (Input.GetKeyDown(KeyCode.Space) && (eggIndex < ammoLimit)) {

				Vector3 fwd = transform.TransformDirection(Vector3.forward);
				audioSource.PlayOneShot(audioClip);
				eggAmmo[eggIndex].SetActive(true);
				eggAmmo[eggIndex].transform.position = eggSource.transform.position;
				Rigidbody eggRgbd = eggAmmo[eggIndex].GetComponent<Rigidbody>();
				playerRdbd.AddExplosionForce(explosionForce, eggSource.position, explosionRadius, upwardsModifier, ForceMode.Force);
				eggRgbd.AddForceAtPosition(fwd * forceMultiply, eggSource.position, ForceMode.Force);
				eggRgbd.useGravity = true;
				eggAmmo[eggIndex].GetComponent<SphereCollider>().isTrigger = false;
				eggIndex++; 

			}

		}*/

	}

	public void ThrowEgg () {

		if (eggIndex < ammoLimit) {

			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			audioSource.PlayOneShot(audioClip);
			eggAmmo[eggIndex].SetActive(true);
			eggAmmo[eggIndex].transform.position = eggSource.transform.position;
			Rigidbody eggRgbd = eggAmmo[eggIndex].GetComponent<Rigidbody>();
			playerRdbd.AddExplosionForce(explosionForce, eggSource.position, explosionRadius, upwardsModifier, ForceMode.Force);
			eggRgbd.AddForceAtPosition(fwd * forceMultiply, eggSource.position, ForceMode.Force);
			eggRgbd.useGravity = true;
			eggAmmo[eggIndex].GetComponent<SphereCollider>().isTrigger = false;
			eggIndex++; 

		}

	}

}
