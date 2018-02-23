using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickControl
{
    public enum JoystickPlayerState
    {
        Connected,
    	Disconnected,
	    Selected
    }

    public class JoystickCanvas : MonoBehaviour
    {
        [SerializeField] JoystickPanel[] panels;

        void Start()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                SetPanel(i, JoystickPlayerState.Disconnected);
            }
        }

        public void SetPanel(int index, JoystickPlayerState state)
        {
            panels[index].SetState(state);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
