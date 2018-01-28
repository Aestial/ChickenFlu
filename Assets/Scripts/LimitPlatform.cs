using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPlatform : MonoBehaviour {


    [SerializeField]
    private int force;
    [SerializeField]
    private float dangerAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("other " + other.name);
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            rigidbody.sleepThreshold = 10f;
            rigidbody.AddForceAtPosition(new Vector3(-1f, 0f, -1f) * force, Vector3.zero);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            rigidbody.sleepVelocity = 1;
            Player player = other.GetComponent<Player>();
            player.UpdateHealth(-dangerAmount);
            

        }

    }
    private void OnTriggerExit(Collider other)
    {
        
    }


}
