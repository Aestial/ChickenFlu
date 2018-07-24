using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

namespace JoystickControl
{
    public class JoystickAssigner : MonoBehaviour
    {
        [SerializeField] private JoystickCanvas m_CanvasController;
        [SerializeField] private GameObject m_StartPanel;
        [SerializeField] private AudioClip selectedClip;
        [SerializeField] private Canvas loadingCanvas;

        private int players;

        void Awake()
        {
            this.loadingCanvas.enabled = false;
        }

        void OnEnable()
        {
            this.players = 0;
            this.DebugJoysticks();
            this.EnableStart(false);
        }

        void Update()
        {
            if (!ReInput.isReady)
                return;
            this.DetectJoysticks();
            this.AssignJoysticks();
            this.CheckPlayerInput();
        }

        private void DebugJoysticks()
        {
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for (int i = 0; i < joysticks.Count; i++)
            {
                Joystick joystick = joysticks[i];
                Debug.Log(joystick.type + " " + joystick.id + ": " + joystick.name + ". Hardware: " + joystick.hardwareName);
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
                    Rewired.Player player = ReInput.players.Players[i];
                    //Rewired.Player player = FindPlayerWithoutJoystick();
                    //if (player == null)
                        //return;
                    players++;
                    Debug.Log("Joined: " + player.descriptiveName);
                    player.controllers.AddController(joystick, false);
                    Debug.Log(player.descriptiveName + " is playing: " + player.isPlaying);
                    AudioManager.Instance.PlayOneShoot2D(selectedClip);
                    this.EnableStart(true);
                }
            }
            // If all players have joysticks, enable joystick auto-assignment
            // so controllers are re-assigned correctly when a joystick is disconnected
            // and re-connected and disable this script
            //if (DoAllPlayersHaveJoysticks())
            //{
            //    ReInput.configuration.autoAssignJoysticks = true;
            //    this.enabled = false; // disable this script
            //    MenuManager.Instance.ChangeScene(1);
            //}
        }

        private void EnableStart(bool active)
        {
            this.m_StartPanel.SetActive(active);

        }

        private void CheckPlayerInput()
        {
            for (int i = 0; i < ReInput.players.playerCount; i++)
            {
                if (ReInput.players.GetPlayer(i).GetButtonDown("Start"))
                {
                    Debug.Log("Some fucker pressed start!");
                    ReInput.configuration.autoAssignJoysticks = true;
                    this.enabled = false; // disable this script
                    PlayerPrefs.SetInt("Players", this.players);
                    this.loadingCanvas.enabled = true;
                    MenuManager.Instance.ChangeScene(1);
                }
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
