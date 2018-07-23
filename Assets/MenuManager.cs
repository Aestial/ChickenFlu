using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager> 
{
	// Use this for initialization
	void Start () 
    {
		
	}

    public void ChangeScene(int scene)
    {
        StartCoroutine(ChangeSceneCoroutine(scene));
    }

    private IEnumerator ChangeSceneCoroutine(int scene)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
