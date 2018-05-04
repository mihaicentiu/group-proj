using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Uniduino;

public class Pause_Game : MonoBehaviour {
	public Transform canvas;
	public Button resumebutton;
	public Button restartbutton;
	public Button menubutton;




	public Arduino arduino;
	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;

	public int buttonPinNumber;
	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;
	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;

	private GameObject ArduinoScript;

	private bool pausebutton;

	private bool toggle;
	private bool ptoggle;

	private int MenuCount; // Counter for pause menu
	private bool valueChange;


	// Update is called once per frame

	void Start () {
		pausebutton = false;
		toggle = false;
		ptoggle = false;

		MenuCount = 2;
		valueChange = false;

		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
		ArduinoScript = GameObject.Find ("Uniduino");


	}

	void ConfigurePins()
	{
		arduino.pinMode (buttonPinNumber, PinMode.INPUT);
		arduino.reportDigital ((byte)(buttonPinNumber / 8), 1);

		arduino.pinMode(joyPinNumber, PinMode.ANALOG);
		arduino.reportAnalog(1, 1);  //up down 
	}

	//Calling the pause
	void Update ()
	{


		if (ArduinoScript.GetComponent<Arduino> ().Connected)	{
			//Debug.Log ("PAUSE BUTTON STATUS:" + arduino.digitalRead (buttonPinNumber));

			if (toggle == false) {
				if (arduino.digitalRead (buttonPinNumber) == 1){
				pausebutton = !pausebutton;
				toggle = true;
				//Debug.Log (toggle + " " + pausebutton);
				}
			}
		}

		if (arduino.digitalRead (buttonPinNumber) == 0)
		{
			toggle = false;
		}

		if ((Input.GetButtonDown("Start Button")) || (pausebutton == true))
		{
			if (ptoggle == false) {
				Pause ();
				resumebutton.Select ();
				resumebutton.OnSelect (null);  
				ptoggle = true;
			}
		  
		}

		if ((ArduinoScript.GetComponent<Arduino> ().Connected) && (ptoggle == true)) {
			MenuControl ();
		}

		if (canvas.gameObject.activeInHierarchy == true) {
			if (Input.GetButtonDown("Cancel")  ||  (pausebutton == false)) {
				Pause ();
				ptoggle = false;
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

	void MenuControl(){

		joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput
		mappedJoy = joyValue.Remap (1023, 0, -1, 1); //changed imput for unity





		if (mappedJoy == -1) {
			if (valueChange == false) {
				MenuCount++;
				valueChange = true;
				if (MenuCount > 3) {
					MenuCount = 3;
				}
			}
		}

		if (mappedJoy == 1) {
			if (valueChange == false) {
				MenuCount--;
				valueChange = true;
				if (MenuCount < 1) {
					MenuCount = 1;
				}
			}
		}

		if ((mappedJoy < 0.2) && ((mappedJoy > -0.2))){
			valueChange = false;

		}


		if (MenuCount == 1) {
			EventSystem.current.SetSelectedGameObject (resumebutton.gameObject); 
		}

		if (MenuCount == 2) {
			EventSystem.current.SetSelectedGameObject (restartbutton.gameObject); 

		}
		if (MenuCount == 3) {
			EventSystem.current.SetSelectedGameObject (menubutton.gameObject); 

		}
	}







	public void Restart_Menu (int scene)
	{
		SceneManager.LoadScene(scene);
		Time.timeScale = 1;
	}


}
