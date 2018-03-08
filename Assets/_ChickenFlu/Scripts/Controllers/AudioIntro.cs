using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioIntro : MonoBehaviour {

	public Image image;
	public Color inColor;
	public Color outColor;
	public bool fadeIn;
	public float speed1, speed2, t0, t1;
	public int nextSceneId;
	public AudioSource audioSource; 


	// Use this for initialization
	void Start () {
		fadeIn = true;
		t0 = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeIn) {
			image.color = Color.Lerp (image.color, inColor, (Time.time - t0) * speed1);
			t1 = Time.time;
		} else {
			image.color = Color.Lerp (image.color, outColor, (Time.time - t1) * speed2);
		}

		if (image.color == inColor) {
			fadeIn = false;
		}

		if (!audioSource.isPlaying && !fadeIn) {
			SceneManager.LoadScene (nextSceneId);
		}

	}
}
