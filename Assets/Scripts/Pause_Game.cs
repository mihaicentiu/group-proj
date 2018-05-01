﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Uniduino;

public class Pause_Game : MonoBehaviour {
	public Transform canvas;
	public Button resumebutton;

	public Arduino arduino;
	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;
	public int joyPinNumber2;
	public int buttonPinNumber;
	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;
	public float joyValue2;
	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;
	public float mappedJoy2;


	private GameObject ArduinoScript;

	private bool pausebutton;

	private bool toggle;

	// Update is called once per frame

	void Start () {
		pausebutton = false;
		toggle = false;
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
		ArduinoScript = GameObject.Find ("Uniduino");
	}

	void ConfigurePins()
	{
		arduino.pinMode (buttonPinNumber, PinMode.INPUT);
		arduino.reportDigital ((byte)(buttonPinNumber / buttonPinNumber), 1);

		arduino.pinMode(joyPinNumber, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber2, PinMode.ANALOG);

		arduino.reportAnalog(1, 1);  //up down

		arduino.reportAnalog(0, 1);  //rotaion of table tilt


	}

	//Calling the pause
	void Update ()
	{


		if (ArduinoScript.GetComponent<Arduino> ().Connected)
		{
			//Debug.Log ("PAUSE BUTTON STATUS:" + arduino.digitalRead (buttonPinNumber));

			if ((toggle == false) && (arduino.digitalRead (buttonPinNumber) == 1))
			{
				pausebutton = !pausebutton;
				toggle = true;
				Debug.Log (toggle + " " + pausebutton);
			}
		}

		if (arduino.digitalRead (buttonPinNumber) == 0)
		{
			toggle = false;
		}

		if ((Input.GetButtonDown("Start Button")) || (pausebutton == true))
		{
			Pause ();
			resumebutton.Select ();
			resumebutton.OnSelect (null);    
		}

		if (canvas.gameObject.activeInHierarchy == true) {
			if (Input.GetButtonDown("Cancel")  ||  (pausebutton == false)) {
				Pause ();
			}
		}

	}




	public void Pause()
	{

		joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput
		mappedJoy = joyValue.Remap (1023, 0, -1, 1); //changed imput for unity

		joyValue2 = arduino.analogRead (joyPinNumber2); //joystick digital imput
		mappedJoy2 = joyValue2.Remap (1023, 0, -1, 1);

		Debug.Log ("MP2:" + mappedJoy2);
		Debug.Log ("MP:"+ mappedJoy);

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
