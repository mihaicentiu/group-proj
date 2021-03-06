﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdTransCeilBox : MonoBehaviour {

	public Arduino arduino;

    private float movrateL;
    private float movrateR;
    private float movrate;
    private float movmult;

	public int buttonPinNumber;
	public int buttonPinNumber2;

	public float leftEdge;
	public float rightEdge;

	public Material Highlight;
	public Material DefaultColour;

	public GameObject ButtonToHighlight;

	private GameObject ArduinoScript;


    // Use this for initialization
    void Start()
	{
		ArduinoScript = GameObject.Find ("Uniduino");
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
		movmult = 0.5f;
		//For highlighting
		Highlight = GameObject.Find ("script").GetComponent<HighlightButtons> ().Highlight;
		DefaultColour = GameObject.Find ("script").GetComponent<HighlightButtons> ().Default07;
    }
		
	void ConfigurePins()
	{
		arduino.pinMode (buttonPinNumber, PinMode.INPUT);
		arduino.reportDigital ((byte)(buttonPinNumber / 8), 1);
		arduino.pinMode (buttonPinNumber2, PinMode.INPUT);
		arduino.reportDigital ((byte)(buttonPinNumber2 / 8), 1);


	}

    // Update is called once per frame
    void FixedUpdate()
    {
			//Debug.Log (arduino.digitalRead (buttonPinNumber) + "pin 8");
			//Debug.Log (arduino.digitalRead (buttonPinNumber2) + "pin 2");
			//Debug.Log (arduino.digitalRead (buttonPinNumber2));
			//Debug.Log (arduino.digitalRead (buttonPinNumber));

			//if ((arduino.digitalRead (buttonPinNumber) == 0) && (arduino.digitalRead (buttonPinNumber2) == 0))  {
			//	movrate = 0;
			//}

			//if ((arduino.digitalRead (buttonPinNumber) == 1) && (transform.localPosition.z <= rightEdge)) {
			//	movrate = movmult;
			//	transform.Translate (0.0f, 0.0f, movrate * Time.deltaTime);
			//	ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			//}
			//else {
			//	ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			//}
				



			//if ((arduino.digitalRead (buttonPinNumber2) == 1) && (transform.localPosition.z >= leftEdge)) {
			//	movrate = -movmult;
			//	transform.Translate (0.0f, 0.0f, movrate * Time.deltaTime);
			//	ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			//}
			//else {
			//	ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			//}

			//Check if arduino connected
			if (ArduinoScript.GetComponent<Arduino> ().Connected) {

			if (((arduino.digitalRead (buttonPinNumber) == 1)) || ((arduino.digitalRead (buttonPinNumber2) == 1))) {
				ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
				if (arduino.digitalRead (buttonPinNumber) == 1) {
					movrate = movmult;
					transform.Translate (0.0f, 0.0f, movrate * Time.deltaTime);
				}
				if (arduino.digitalRead (buttonPinNumber2) == 1) {
					movrate = -movmult;
					transform.Translate (0.0f, 0.0f, movrate * Time.deltaTime);
				}
			}
				else {
						ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
					}
			} 
			}


		}

		//Debug.Log (transform.localPosition.z);
	
	