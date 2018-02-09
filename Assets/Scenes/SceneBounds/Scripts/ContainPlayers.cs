using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainPlayers : MonoBehaviour {

	/// <summary>
	/// Detects if a player leaves the battlefield zone and instantiates
	/// a weapon that turns back the player to the battlefield zone.
	/// </summary>

	public GameObject[] weapon;
	private Vector3 weaponPosition;
	private Vector3 weaponOffset = new Vector3(0, 3, 0);
	private int weaponIndex;

	void Start () {

		weaponIndex = 0;
		weapon = new GameObject[transform.childCount];

		for (int i=0; i < transform.childCount; i++) {
			weapon[i] = transform.GetChild(i).gameObject;
			weapon[i].SetActive(false);
		}

	}

	void OnTriggerEnter(Collider other){
		
		Debug.Log(other.name);
		if (other.tag == "Player") {
			ReturnToTerrain(other.transform.position);
		}

	}

	void ReturnToTerrain (Vector3 playerPosition) {

		weaponPosition = playerPosition + weaponOffset;
		Debug.Log("Calculate position");
		//GameObject newWeapon = Instantiate(weaponPrefab, weaponPosition, Quaternion.identity);

		if (!weapon[weaponIndex].activeInHierarchy) {

			Debug.Log("Enable weapon");
			weapon[weaponIndex].SetActive(true);
			weapon[weaponIndex].transform.position = weaponPosition;
			WeaponController weaponController = weapon[weaponIndex].GetComponent<WeaponController>();
			weaponController.disableWeapon = true;
			weaponController.t0 = Time.time;
			weaponController.AimPlayer(playerPosition);

			if (weaponIndex < transform.childCount - 1) {
				weaponIndex++;
			} else {
				weaponIndex = 0;
			}

		}

	}
}
