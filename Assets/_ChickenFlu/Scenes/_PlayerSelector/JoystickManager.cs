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
        [SerializeField] private JoystickAssigner assigner;

        void Start()
        {
            this.assigner.enabled = false;
        }

        // Update is called once per frame
        void Update () 
		{
            if (!ReInput.isReady)
                return;
			this.CheckJoystickInput();	
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
                    Debug.Log("Someone pressed a button!");
                    this.assigner.enabled = true;
					this.UI.ChangeScreen(1);
                    this.enabled = false;
				}
			}
		}
	}
}
