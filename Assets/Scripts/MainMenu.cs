using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public Text unavailable;



	public void Control(int scene) {
		unavailable.gameObject.SetActive (false);
		SceneManager.LoadScene(scene);
	}




	public void Other ()
	{
		unavailable.gameObject.SetActive (true);
	}





	public void QuitGame () {
		unavailable.gameObject.SetActive (false);
		Application.Quit();
	}﻿
}
