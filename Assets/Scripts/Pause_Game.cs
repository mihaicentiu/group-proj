using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Game : MonoBehaviour {
	public Transform canvas;
	public Button resumebutton;

	
	// Update is called once per frame

	//Calling the pause
	void Update () {
		if (Input.GetButtonDown("Start Button"))
		{
			Pause ();
			resumebutton.Select ();
			resumebutton.OnSelect (null);	
		}

		if (canvas.gameObject.activeInHierarchy == true){
			if (Input.GetButtonDown("Cancel")){
				Pause ();
			}
		}
	}



	public void Pause()
	{
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		}

		else 
		{
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}





	public void Restart_Menu (int scene)
	{
		SceneManager.LoadScene(scene);
		Time.timeScale = 1;
	}


}
