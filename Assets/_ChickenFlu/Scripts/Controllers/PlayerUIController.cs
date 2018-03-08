using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour 
{
    [SerializeField] private Text healthText;
	
    public void UpdateHealth(float health)
    {
        int percent = Mathf.RoundToInt(health * 100.0f);
        this.healthText.text = percent.ToString() + "%";
    }
}