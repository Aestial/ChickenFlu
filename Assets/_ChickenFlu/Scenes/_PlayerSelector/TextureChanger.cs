using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour 
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Texture2D texture;

	// Use this for initialization
	void Start () 
    {
        this.renderer.material.mainTexture = this.texture;
	}
}
