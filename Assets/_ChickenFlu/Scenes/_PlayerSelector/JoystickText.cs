using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickControl
{
    public class JoystickText : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerState state;

        [Multiline][SerializeField] private string m_Disconnected;
        [Multiline][SerializeField] private string m_Connected;
        [Multiline][SerializeField] private string m_Selected;
		private Text text;

        public void SetState(JoystickPlayerState state)
        {
            switch (state)
            {
                case JoystickPlayerState.Connected:
                    this.text.text = m_Connected;
                    break;
                case JoystickPlayerState.Disconnected:
                    this.text.text = m_Disconnected;
                    break;
                case JoystickPlayerState.Selected:
                    this.text.text = m_Selected;
                    break;
            }
            this.state = state;
        }

        void Awake()
        {
            this.text = GetComponent<Text>();
        }
    }
}