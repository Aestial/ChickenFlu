using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour 
{
    [SerializeField] private bool destroyOnAwake;
	void Awake () 
    {
        if (destroyOnAwake)
        {
            Destroy(this.gameObject);     
        }
	}
}
