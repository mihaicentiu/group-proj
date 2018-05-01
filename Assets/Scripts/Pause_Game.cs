using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Uniduino;

public class Pause_Game : MonoBehaviour {
	public Transform canvas;
	public Button resumebutton;

	public Arduino arduino;

	public int buttonPinNumber;

	private GameObject ArduinoScript;

	private bool unlockTilt;

	private bool toggle;

	// Update is called once per frame

	void start () {
		unlockTilt = false;
		toggle = false;
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
	}

	void ConfigurePins()
	{
		arduino.pinMode (buttonPinNumber, PinMode.INPUT);
		arduino.reportDigital ((byte)(buttonPinNumber / buttonPinNumber), 1);
	}

	//Calling the pause
	void Update ()
	{
		Debug.Log("PAUSE BUTTON STATUS:" + (arduino.digitalRead (buttonPinNumber)));

		if (ArduinoScript.GetComponent<Arduino> ().Connected)
		{

			if (toggle == false) {
				if (arduino.digitalRead (buttonPinNumber) == 1) {
					unlockTilt = !unlockTilt;
					toggle = true;
					Debug.Log (toggle + " " + unlockTilt);
				}
			}

			if (arduino.digitalRead (buttonPinNumber) == 0) {
				toggle = false;
			}

		if ((Input.GetButtonDown("Start Button")) || (unlockTilt == true))
		{
			Pause ();
			resumebutton.Select ();
			resumebutton.OnSelect (null);	
		}

			if (canvas.gameObject.activeInHierarchy == true) {
				if (Input.GetButtonDown("Cancel")  ||  (unlockTilt == false)) {
				Pause ();
			}
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
