using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour 
{
    //[SerializeField] private bool startActive;
    private Canvas canvas;

    void Awake()
    {
        this.canvas = GetComponent<Canvas>();
        this.canvas.enabled = false;
    }
}
