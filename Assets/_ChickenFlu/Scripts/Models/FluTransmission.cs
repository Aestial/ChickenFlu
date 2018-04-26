using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluTransmission : MonoBehaviour 
{
    [SerializeField] private float infectSafeTime = 1.0f;
    [SerializeField] private GameObject particleFX;
    [SerializeField] private float headHeight = 0.6f;
    [SerializeField] private AudioClip audioFX;

    private Player player;
	
	void Start () 
    {
        this.player = GetComponent<Player>();
	}

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.transform.tag == "Player" && 
            (this.player.State == PlayerState.Infected ||
             this.player.State == PlayerState.MadChicken))
        {
            Player other = collision.transform.GetComponent<Player>();
            if (other.CanBeInfected)
            {

                //Debug.Log("Flu Trans - Transmited to: " + other.transform.name);    
				GameManager.Instance.Infect(other.Id);
                this.PlayEffect(other);
                StartCoroutine(this.InfectSafe());
            }
        }
	}
    private void PlayEffect(Player other) 
    {
        AudioManager.Instance.PlayOneShoot(audioFX, other.transform.position);  
        Instantiate(particleFX, other.transform.position + new Vector3(0, 1), Quaternion.identity);
    }
    // public void PlayEffect() 
    // {
    //     this.player.Mutate(PlayerState.Infected);
    //     Vector3 headPos = this.transform.position + new Vector3(0, headHeight);
    //     Vector3 offset = headPos - Camera.main.transform.position;
    //     offset = -offset.normalized;
    //     Instantiate(particleFX, headPos + offset, Quaternion.identity);
    //     AudioManager.Instance.PlayOneShoot(audioFX, this.transform.position);  

    // }
    private IEnumerator InfectSafe()
    {
        this.player.CanBeInfected = false;
        yield return new WaitForSeconds(infectSafeTime);
        this.player.CanBeInfected = true;
    }

}
