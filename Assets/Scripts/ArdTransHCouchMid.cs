using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdTransHCouchMid : MonoBehaviour {
	[Header("Arduino Variables")]

	//we need to declare the Arduino as a variable
	public Arduino arduino;

	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;

	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;

	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;

	[Header("Table Variables")]

	//variables to hold the values we noted earlier for the sides of our screen

    private float hcouchmidmovrate;
    private float hcouchmidmovmult;

	private Material Highlight;
	private Material DefaultColour;

	public GameObject ButtonToHighlight;

	private GameObject ArduinoScript;

    // Use this for initialization
    void Start()
    {
        hcouchmidmovmult = 0.05f;
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

		//Tell the Arduino to report any changes in the value of our joysticks2
		arduino.reportAnalog(13, 1);  //up down
	}

    // Update is called once per frame
    void Update ()
	{
		//Check if arduino connected
		if (ArduinoScript.GetComponent<Arduino> ().Connected) {
			
			joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput

			mappedJoy = joyValue.Remap (1023, 0, 1, -1); //changed imput for unity

			if ((joyValue <= 477) || (joyValue >= 569)) {

				hcouchmidmovrate = -mappedJoy * hcouchmidmovmult;
				transform.Translate (hcouchmidmovrate * Time.deltaTime, 0.0f, 0.0f);
				ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			} else {
				ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			}
		}
	}
}
