using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour {

	public int eggIdentifyer;				//To identiffy the player eggs and prevent the collision
	public GameObject openParticles;

	void OnCollisionEnter(Collision other) {

		if (other.transform.tag == "Player") {

			int eggId = other.transform.GetComponent<Player>().Id;

			if (eggIdentifyer != eggId) {
				float randomValue = Random.value;
				Debug.Log ("Egg hit player");
				StartCoroutine(SpawnItem(randomValue));
			}
			
		}

	}

	IEnumerator SpawnItem (float randomValue) {

		if(randomValue > 0.45f) { //%55 percent chance

			ResourceRequest asyncLoad = Resources.LoadAsync< GameObject >( "Cuckoo" );		//Neutral item
			asyncLoad.priority = ( int ) ThreadPriority.Normal;

			// waits until the resource is loaded
			yield return asyncLoad;

			if ( asyncLoad.asset == null ) {
				// logging on different colors is a nice way to make people pay a bit more attention to what's happening on console
				Debug.Log("Couldn't load the object.");
				yield break;
			}

			// creates an instance of the loaded prefab on the scene
			Instantiate(openParticles, transform.position, Quaternion.identity);
			Instantiate(asyncLoad.asset, transform.position, Quaternion.identity );
			//Debug.Log("Cuckoo");
			Destroy(gameObject);

		} else {

			if(randomValue > 0.25f) { //%30 percent chance (1 - 0.7 is 0.3)
				
				ResourceRequest asyncLoad = Resources.LoadAsync< GameObject >( "Vaccine" );	//Bad item
				asyncLoad.priority = ( int ) ThreadPriority.Normal;

				// waits until the resource is loaded
				yield return asyncLoad;

				if ( asyncLoad.asset == null ) {
					// logging on different colors is a nice way to make people pay a bit more attention to what's happening on console
					Debug.Log("Couldn't load the object.");
					yield break;
				}

				// creates an instance of the loaded prefab on the scene
				Instantiate(openParticles, transform.position, Quaternion.identity);
				Instantiate(asyncLoad.asset, transform.position, Quaternion.identity );
				//Debug.Log("BadSurprice");
				Destroy(gameObject);

			} else {
				
				ResourceRequest asyncLoad = Resources.LoadAsync< GameObject >( "BadSurprice" );	//Help item
				asyncLoad.priority = ( int ) ThreadPriority.Normal;

				// waits until the resource is loaded
				yield return asyncLoad;

				if ( asyncLoad.asset == null ) {
					// logging on different colors is a nice way to make people pay a bit more attention to what's happening on console
					Debug.Log("Couldn't load the object.");
					yield break;
				}

				// creates an instance of the loaded prefab on the scene
				Instantiate(openParticles, transform.position, Quaternion.identity);
				Instantiate(asyncLoad.asset, transform.position, Quaternion.identity );
				//Debug.Log("Vaccine");
				Destroy(gameObject);

			}

		}

	}

}
