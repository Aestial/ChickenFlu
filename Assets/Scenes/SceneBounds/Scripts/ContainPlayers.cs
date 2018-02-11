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
	private Vector3 playerPosition;

	private bool generateExplotion;
	public float explotionDelay;
	private float explotionCounter;
	public float bulletForce;
	public float bulletRadius;
	private Rigidbody rgbd;
	public Vector3 explotionOffset;				//Offset to generate the explotion to pull back the player to the battlefield, depends of the wall orientation

	//public GameObject testObj;

	void Start () {

		weaponIndex = 0;
		weapon = new GameObject[transform.childCount];

		for (int i=0; i < transform.childCount; i++) {
			weapon[i] = transform.GetChild(i).gameObject;
			weapon[i].SetActive(false);
		}

		generateExplotion = false;
		explotionCounter = 0;

	}

	void OnTriggerEnter(Collider other){
		
		Debug.Log(other.name);
		if (other.tag == "Player") {
			playerPosition = other.transform.position;
			ReturnToTerrain();
			rgbd = other.GetComponent<Rigidbody> ();
		}

	}

	void ReturnToTerrain () {

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

			generateExplotion = true;

		}

	}

	void Update () {

		if (generateExplotion) {

			if (explotionCounter < explotionDelay) {
				explotionCounter += Time.time * 0.2f;
			} else {
				ApplyExplotion ();
			}

		}

	}

	void ApplyExplotion () {

		//Instantiate(testObj, playerPosition + explotionOffset, Quaternion.identity);
		rgbd.AddExplosionForce (bulletForce, playerPosition + explotionOffset, bulletRadius);
		generateExplotion = false;
		explotionCounter = 0;

	}

}
