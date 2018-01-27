using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluTransmission : MonoBehaviour 
{
    private Player player;
	
	void Start () 
    {
        this.player = GetComponent<Player>();
	}
	
	void Update () 
    {
		
	}

	void OnCollisionEnter(Collision col) 
    {
        if (col.transform.tag == "Player")
        {
            Debug.Log("Flu Trans - Collided with: " + col.transform.name);    
            if (this.player.State == PlayerState.Infected ||
                this.player.State == PlayerState.MadChicken)
            {
                Debug.Log("Flu Trans - Transmited to: " + col.transform.name);    
                Player other = col.transform.GetComponent<Player>();
                GameManager.Instance.Infect(other.Number);
            }
        }
	}

	public void Infected() 
    {

	}

}
