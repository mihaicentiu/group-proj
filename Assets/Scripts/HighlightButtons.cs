using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class HighlightButtons : MonoBehaviour {

	//Left Stick
	//public GameObject LArmHorizButton;
	//public GameObject LArmUpButton;
	//public GameObject IntensifierUpDownButton;

	//Right Stick
	//public GameObject CArmRotation;  //left 
	//public GameObject CArmAntiClockButton; //right
	public GameObject CArmRotAndCradleRotButton;


	//Dpad
	//public GameObject Table;
	//public GameObject TableDownButton;
	//public GameObject TableForwardButton;
	//public GameObject TableBackButton;

	//TriggerL
	//public GameObject CeilingBoxForwardBack;
	//TriggerR
	//public GameObject CeilingBoxForward;

	public Material Highlight;
	public Material Default01;
	public Material Default07;
	public Material Default08;

	public Arduino arduino;

	public int joyPinVertical;
	public int joyPinHorizontal;

	public float joyValueVertical;
	public float joyValueHorizontal;

	public float mappedVert;
	public float mappedHoriz;

	private GameObject ArduinoScript;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		ArduinoScript = GameObject.Find ("Uniduino");
				
	}

	void ConfigurePins()
	{
		//configure the Arduino pin to be analog for our joysticks
		arduino.pinMode(joyPinHorizontal, PinMode.ANALOG);
		arduino.reportAnalog(joyPinHorizontal, 1);  //up down

		arduino.pinMode(joyPinVertical, PinMode.ANALOG);

		arduino.reportAnalog(joyPinVertical, 1);
	}
	
	// Update is called once per frame
	void Update () {


//		if ((Input.GetAxis ("VerticalL") > 0.2) || (Input.GetAxis ("VerticalL") < -0.2) )  {
//			IntensifierUpDownButton.GetComponent<Renderer> ().material = Highlight;
//		}
//		else if ((Input.GetAxis ("VerticalL") < 0.2) && (Input.GetAxis ("VerticalL") > -0.2) ) {
//			IntensifierUpDownButton.GetComponent<Renderer> ().material = Default01;
//		}

		if (ArduinoScript.GetComponent<Arduino> ().Connected) {
				joyValueVertical = arduino.analogRead (joyPinVertical); //joystick digital imput
				mappedVert = joyValueVertical.Remap (1023, 0, -1, 1);

				joyValueHorizontal = arduino.analogRead (joyPinHorizontal); //joystick digital imput
				mappedHoriz = joyValueVertical.Remap (1023, 0, -1, 1);

				if (((joyValueVertical <= 250) || (joyValueVertical >= 750)) || ((joyValueHorizontal <= 250) || (joyValueHorizontal>= 750))) {
					CArmRotAndCradleRotButton.GetComponent<Renderer> ().material = Highlight;
					//Debug.Log ("HIGHLIGHT!!!!");

				}


				else{
					CArmRotAndCradleRotButton.GetComponent<Renderer> ().material = Default01;

					//Debug.Log ("STOP HIGHLIGHT!!!!");
				}




		
	}
}
}