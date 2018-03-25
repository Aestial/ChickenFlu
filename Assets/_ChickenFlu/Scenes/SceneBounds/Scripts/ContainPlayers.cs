using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainPlayers : MonoBehaviour {

	/// <summary>
	/// Detects if a player leaves the battlefield zone and instantiates
	/// a weapon that turns back the player to the battlefield zone.
	/// </summary>

	public Transform weaponsContainer;
	public GameObject[] weapon;
	private Vector3 weaponPosition;
	private Vector3 weaponOffset = new Vector3(0, 3, 0);
	private int weaponIndex;
	private GameObject playerObj;

	private bool generateExplotion;
	public float explotionDelay;
	private float explotionCounter;
	public float bulletForce;
	public float bulletRadius;
	private Rigidbody rgbd;
	public Vector3 explotionOffset;				//Offset to generate the explotion to pull back the player to the battlefield, depends of the wall orientation

	private InputController[] inputController = new InputController[4];
	private int playerIndex;
	public float timeDisabled;
	//public GameObject testObj;

	void Start () {

		weaponIndex = 0;
		weapon = new GameObject[weaponsContainer.childCount];

		for (int i=0; i < weaponsContainer.childCount; i++) {
			weapon[i] = weaponsContainer.GetChild(i).gameObject;
			weapon[i].SetActive(false);
		}

		generateExplotion = false;
		explotionCounter = 0;
		playerIndex = 0;

	}

	void OnTriggerEnter(Collider other){
		
		Debug.Log(other.name);
		if (other.tag == "Player") {

			playerObj = other.gameObject;
			rgbd = other.GetComponent<Rigidbody> ();

			ReturnToTerrain();
			DisableMovement(other.gameObject);

		}

	}

	void ReturnToTerrain () {

		weaponPosition = playerObj.transform.position + weaponOffset;
		Debug.Log("Calculate position");

			Debug.Log("Enable weapon");
			weapon[weaponIndex].SetActive(true);
			weapon[weaponIndex].transform.position = weaponPosition;
			WeaponController weaponController = weapon[weaponIndex].GetComponent<WeaponController>();
			weaponController.disableWeapon = true;
			weaponController.t0 = Time.time;
			weaponController.AimPlayer(playerObj.transform.position);

			if (weaponIndex < transform.childCount - 1) {
				weaponIndex++;
			} else {
				weaponIndex = 0;
			}

			generateExplotion = true;

		//}

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

		playerObj.GetComponent<VFX>().InstantiatePSystem();
		rgbd.AddExplosionForce (bulletForce, playerObj.transform.position + explotionOffset, bulletRadius);
		generateExplotion = false;
		explotionCounter = 0;

	}

	private void DisableMovement(GameObject other) {

		inputController[playerIndex] = other.GetComponent<InputController>();
		inputController[playerIndex].enabled = false;

		//Disable movement script
		StartCoroutine(ReturnMovement(playerIndex));
	}

	private IEnumerator ReturnMovement (int plyrIndex) {

		if (playerIndex < 3) {
			playerIndex++;
		} else {
			playerIndex = 0;
		}

    	yield return new WaitForSeconds(timeDisabled);
       	//Enable movement script
		inputController[plyrIndex].enabled = true;

    }

}
