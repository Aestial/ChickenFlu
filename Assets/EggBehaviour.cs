using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour {

	public int eggIdentifyer;				//To identiffy the player eggs and prevent the collision

	void OnCollisionEnter(Collision other) {

		if (other.transform.tag == "Player") {

			int eggId = other.transform.GetComponent<EggController>().eggIdentifier;

			if (eggIdentifyer != eggId) {
				Debug.Log ("Egg hit player");
			}
			
		}

	}

}
