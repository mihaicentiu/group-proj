using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour {

	public Slider slider;
	public Text progressText;

	public void PlayGame(int scene) 
	{
		StartCoroutine (LoadAsynchronously (scene));
	}

	IEnumerator LoadAsynchronously (int scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		slider.gameObject.SetActive (true);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			slider.value = progress;
			progressText.text = progress * 100f + "%";
			yield return null;
		}
	}

	public void QuitGame () {
		Application.Quit();
	}﻿
}
