using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickControl
{
    [RequireComponent(typeof(Image))]
    public class JoystickImage : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerState state;
        [SerializeField] private Color m_Active;
        [SerializeField] private Color m_Inactive;
        [SerializeField] private Color m_Selected;
        private Image image;

        public void SetState(JoystickPlayerState state)
        {
            switch (state)
            {
                case JoystickPlayerState.Connected:
                    this.image.color = m_Active;
                    break;
                case JoystickPlayerState.Disconnected:
                    this.image.color = m_Inactive;
                    break;
                case JoystickPlayerState.Selected:
                    this.image.color = m_Selected;
                    break;
            }
            this.state = state;
        }

        void Awake()
        {
            this.image = GetComponent<Image>();
        }

        void Start()
        {
            // SetState(this.state);
        }

        void Update()
        {

        }
    }

}