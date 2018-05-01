using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdTransIntens : MonoBehaviour {

    private float intensmovrate;
    private float intensmovmult;

	public Arduino arduino;

	public int joyPinNumber;

	public float joyValue;


	public float mappedJoy;
	public Material Highlight;
	public Material DefaultColour;

	public GameObject ButtonToHighlight;

	private GameObject ArduinoScript;


    // Use this for initialization
    void Start()
    {
        intensmovmult = 0.1f;
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

		arduino.reportAnalog(3, 1);  //up down
	}

    // Update is called once per frame
    void Update()
	{
		//Check if arduino connected
		if (ArduinoScript.GetComponent<Arduino> ().Connected) {


			joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput

			mappedJoy = joyValue.Remap (1023, 0, -1, 1);

			intensmovrate = Input.GetAxis ("VerticalL") * intensmovmult;
        
			intensmovrate = mappedJoy * intensmovmult;
	
			if (transform.localPosition.y >= 1.17f) {
				intensmovrate = -0.01f;
				ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			} else {
				ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			}

			if (transform.localPosition.y <= 0.65f) {
				intensmovrate = +0.01f;
				ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			} else {
				ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			}

			transform.Translate (0.0f, intensmovrate * Time.deltaTime, 0.0f);
		}
	}
}