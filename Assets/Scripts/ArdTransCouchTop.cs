﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdTransCouchTop : MonoBehaviour {
	[Header("Arduino Variables")]

	//we need to declare the Arduino as a variable
	public Arduino arduino;

	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;
	public int joyPinNumber2;
	public int rotateButtonNumber;
	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;
	public float joyValue2;

	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;
	public float mappedJoy2;

	public float spinSpeed;

	public float bottomEdge;
	public float topEdge;

    private float vcouchmovrate;
    private float vcouchmovmult;

	private bool unlockTilt;

	private bool toggle;

	private Material Highlight;
	private Material DefaultColour;

	public GameObject ButtonToHighlight;
	public GameObject ButtonToggleHighlight;

	private GameObject ArduinoScript;

    // Use this for initialization
    void Start()
    {
		unlockTilt = false;
		toggle = false;
        vcouchmovmult = 0.05f;
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
		//For highlighting
		Highlight = GameObject.Find ("script").GetComponent<HighlightButtons> ().Highlight;
		DefaultColour = GameObject.Find ("script").GetComponent<HighlightButtons> ().Default01;
		//grab arduino object
		ArduinoScript = GameObject.Find ("Uniduino");
    }


	void ConfigurePins()
	{
		//configure the Arduino pin to be analog for our joysticks
		arduino.pinMode(joyPinNumber, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber2, PinMode.ANALOG);
		arduino.pinMode(rotateButtonNumber, PinMode.INPUT);
		//Tell the Arduino to report any changes in the value of our joysticks2
		arduino.reportAnalog(1, 1);  //up down
		arduino.reportAnalog(0, 1);  //rotaion of table tilt
		arduino.reportDigital((byte)(rotateButtonNumber / 8), 1); //unlock table tilt
		 
	}

    // Update is called once per frame
    void Update()
	{
		//Check if arduino connected
		if (ArduinoScript.GetComponent<Arduino> ().Connected) {
			
			joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput
			mappedJoy = joyValue.Remap (1023, 0, -1, 1); //changed imput for unity

			joyValue2 = arduino.analogRead (joyPinNumber2); //joystick digital imput
			mappedJoy2 = joyValue2.Remap (1023, 0, -1, 1);


			vcouchmovrate = mappedJoy * vcouchmovmult;

			//Debug.Log (arduino.digitalRead (rotateButtonNumber));

			float RotX = transform.rotation.x;

			//Toggle button
			if ((toggle == false) && (arduino.digitalRead (rotateButtonNumber) == 1)) {
				unlockTilt = !unlockTilt;
				toggle = true;
				Debug.Log (arduino.digitalRead (rotateButtonNumber));
				//toggle button highlight
				ButtonToggleHighlight.GetComponent<Renderer> ().material = Highlight;
			} 

			if (arduino.digitalRead (rotateButtonNumber) == 0) {
				toggle = false;
				ButtonToggleHighlight.GetComponent<Renderer> ().material = DefaultColour;
			}

			if ((RotX < topEdge) || (RotX > bottomEdge)) {
				if (unlockTilt == false) {

					if ((joyValue <= 477) || (joyValue >= 569)) {
						if (transform.localPosition.y >= 0.74f) {
							vcouchmovrate = -0.5f;

						}

						if (transform.localPosition.y <= 0.37f) {
							vcouchmovrate = 0.5f;
						}
						transform.Translate (0.0f, vcouchmovrate * Time.deltaTime, 0.0f);
						//highlight button
						ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
					} else {
						ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
					}
				
			
				}
			}






			if (unlockTilt == true) {
				

				if ((joyValue2 >= 750) || (joyValue2 <= 250)) {
					transform.RotateAround (transform.position, Vector3.right, mappedJoy2 * spinSpeed);
					//highlight button
					ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
				} else {
					ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
				}
				
			}
		}
	}
	}