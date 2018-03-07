using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

namespace JoystickControl
{
    public class JoystickAssigner : MonoBehaviour
    {
        [SerializeField] private JoystickCanvas m_CanvasController;
        [SerializeField] private AudioClip selectedClip;
        [SerializeField] private AudioClip enabledClip;

        void Update()
        {
            if (!ReInput.isReady)
                return;
            this.DebugJoysticks();
            this.DetectJoysticks();
            this.AssignJoysticks();
        }
        private void DebugJoysticks()
        {
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for (int i = 0; i < joysticks.Count; i++)
            {
                Joystick joystick = joysticks[i];
                Debug.Log(joystick.type + " " + joystick.id + ": " + joystick.hardwareName);
            }
        }
        private void DetectJoysticks()
        {
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for (int i = 0; i < joysticks.Count; i++)
            {
                Joystick joystick = joysticks[i];
                this.m_CanvasController.SetPanel(i, JoystickPlayerState.Connected);
            }
            for (int i = joysticks.Count; i < 4; i++ )
            {
                this.m_CanvasController.SetPanel(i, JoystickPlayerState.Disconnected);
            }
        }
        private void AssignJoysticks()
        {
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for (int i = 0; i < joysticks.Count; i++)
            {
                Joystick joystick = joysticks[i];
                if (ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id)) 
                {
                    this.m_CanvasController.SetPanel(i, JoystickPlayerState.Selected);
                    continue;
                }
                if (joystick.GetAnyButtonDown())
                {
                    Rewired.Player player = FindPlayerWithoutJoystick();
                    if (player == null)
                        return;
                    player.controllers.AddController(joystick, false);
                    AudioManager.Instance.PlayOneShoot2D(selectedClip);
                }
            }
            // If all players have joysticks, enable joystick auto-assignment
            // so controllers are re-assigned correctly when a joystick is disconnected
            // and re-connected and disable this script
            if (DoAllPlayersHaveJoysticks())
            {
                ReInput.configuration.autoAssignJoysticks = true;
                this.enabled = false; // disable this script
            }
        }

        private Rewired.Player FindPlayerWithoutJoystick()
        {
            IList<Rewired.Player> players = ReInput.players.Players;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].controllers.joystickCount > 0)
                    continue;
                return players[i];
            }
            return null;
        }

        private bool DoAllPlayersHaveJoysticks()
        {
            return this.FindPlayerWithoutJoystick() == null;
        }
    }
}
