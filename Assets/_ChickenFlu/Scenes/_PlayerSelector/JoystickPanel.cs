using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickControl
{
    public class JoystickPanel : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerState state;
        [SerializeField] private JoystickImage background;
        [SerializeField] private JoystickImage icon;
        [SerializeField] private JoystickText message;

        void Start()
        {
            SetState(this.state);
        }

        public void SetState(JoystickPlayerState state)
        {
            this.background.SetState(state);
            this.icon.SetState(state);
			this.message.SetState(state);

            this.state = state;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
