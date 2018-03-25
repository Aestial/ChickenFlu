using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioController : MonoBehaviour {
	[SerializeField] private AudioClip[] playerOptions;

	public void PlaySound (int index) {
		AudioManager.Instance.PlayOneShoot2D(playerOptions[index]);
	}
}
