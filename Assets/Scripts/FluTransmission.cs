using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluTransmission : MonoBehaviour 
{
    [SerializeField] private float infectSafeTime = 1.0f;
    private Player player;
	
	void Start () 
    {
        this.player = GetComponent<Player>();
	}
	
	void Update () 
    {
		
	}

	void OnCollisionExit(Collision col) 
    {
        if (col.transform.tag == "Player")
        {
            //Debug.Log("Flu Trans - Collided with: " + col.transform.name);    
            if (this.player.State == PlayerState.Infected ||
                this.player.State == PlayerState.MadChicken)
            {
                Player other = col.transform.GetComponent<Player>();
                if (other.CanBeInfected)
                {
                    Debug.Log("Flu Trans - Transmited to: " + other.transform.name);    
                    GameManager.Instance.Infect(other.Number);
                    StartCoroutine(this.InfectSafe());
                }
            }
        }
	}

    private IEnumerator InfectSafe()
    {
        this.player.CanBeInfected = false;
        yield return new WaitForSeconds(infectSafeTime);
        this.player.CanBeInfected = true;
    }
	
    public void Infected() 
    {

	}

}
