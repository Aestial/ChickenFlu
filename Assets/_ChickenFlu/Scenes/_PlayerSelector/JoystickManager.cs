using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

namespace JoystickControl
{
	public class JoystickManager : MonoBehaviour 
	{
		[SerializeField] private AudioClip readyFXClip;

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (!ReInput.isReady)
				return;
			this.CheckInput();	
		}

		private void CheckInput()
		{
			for(int i = 0; i < ReInput.players.playerCount; i++) 
			{
				if(ReInput.players.GetPlayer(i).GetButtonDown("Start")) 
				{
					Debug.Log("Some fucker pressed start!");
					AudioManager.Instance.PlayOneShoot2D(this.readyFXClip);

				}
			}
		}
	}
}
