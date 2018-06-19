using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

namespace JoystickControl
{
	public class JoystickManager : MonoBehaviour 
	{
		[SerializeField] private AudioClip readyFXClip;
		[SerializeField] private UIScreenManager UI;

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (!ReInput.isReady)
				return;
			this.CheckJoystickInput();
			// this.CheckPlayerInput();	
		}

		private void CheckJoystickInput()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				Joystick joystick = joysticks[i];
				if (joystick.GetAnyButtonDown())
				{
					AudioManager.Instance.PlayOneShoot2D(this.readyFXClip);
					this.UI.ChangeScreen(1);
				}
			}
		}

		private void CheckPlayerInput()
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
