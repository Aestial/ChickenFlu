using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour 
{
	[SerializeField] private Canvas[] screenCanvases;

	private Canvas currentCanvas;
	private int currentIndex;

	// Use this for initialization
	void Start () 
	{
		this.currentIndex = 0;
		this.EnableAll(false);
		this.ChangeScreen(this.currentIndex);	
	}

	private void EnableAll(bool enabled)
	{
		for (int i = 0; i < this.screenCanvases.Length; i++)
		{
			this.screenCanvases[i].enabled = false;
		}
	}

	public void ChangeScreen(int index)
	{
		if (this.currentCanvas)
			this.currentCanvas.enabled = false;
		this.currentCanvas = this.screenCanvases[index];
		this.currentCanvas.enabled = true;
		this.currentIndex = index;
	}
}
