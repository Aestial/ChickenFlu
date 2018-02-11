using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	/// <summary>
	/// Control the animation states of the weapon.
	/// Set the instantiated particle system, in the inspector.
	/// </summary>

	public GameObject particleSystemPrefab;
	public Transform pSystemSource;
	private Animator animator;
	private float shootForce = 16f;
	public bool disableWeapon;
	public float weaponLife;
	public float lifeCount;
	public float t0;


	void Start () {
		disableWeapon = false;
		lifeCount = weaponLife;
	}

	void Update() {

		if (disableWeapon) {
			DisableWeapon ();
		}
		
	}

	public void AimPlayer (Vector3 playerPosition) {

		transform.LookAt(playerPosition);
		Shoot();

	}

	void Shoot() {

		if (name == "Flamethrower") {
			
			GameObject weaponPSystem = Instantiate (particleSystemPrefab, pSystemSource.position, Quaternion.identity);
			weaponPSystem.transform.SetParent (pSystemSource);

		} else {
			
			GameObject weaponPSystem = Instantiate (particleSystemPrefab, pSystemSource.position, Quaternion.identity);
			weaponPSystem.transform.SetParent (pSystemSource);
			Rigidbody systemRigidbody = weaponPSystem.GetComponent<Rigidbody>();
			systemRigidbody.AddForce(transform.forward * shootForce, ForceMode.Force);

		}

	}

	public void DisableWeapon () {

		if (lifeCount > 0) {
			lifeCount -= (Time.time - t0) ;
		} else {
			lifeCount = weaponLife;
			disableWeapon = false;
			gameObject.SetActive(false);
		}

	}

}
