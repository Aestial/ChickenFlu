using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType 
{
    Vaccine
}

public class PowerUpController : MonoBehaviour 
{
    [Range(0.0f, 1.0f)] [SerializeField] private float amount;

	void Start () 
    {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            switch (player.State)
            {
                case PlayerState.Human:
                    player.UpdateHealth(amount);
                    break;
                case PlayerState.Infected:
                    player.UpdateHealth(amount);
                    break;
                case PlayerState.Chicken:
                    player.Mutate(PlayerState.Human);
                    player.UpdateHealth(amount);
                    break;
                case PlayerState.MadChicken:
                    player.Mutate(PlayerState.Human);
                    player.UpdateHealth(amount);
                    break;
                default:
                    break;
            }
            Destroy(this.transform.parent.gameObject);
        }
    }

    void Update () {
		
	}
}
