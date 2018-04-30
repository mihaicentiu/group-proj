using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdRotnCArm : MonoBehaviour {


	public Arduino arduino;
    private float rotrate;
    private float rotmult;
	private float rotationX = 0f;

	public int joyPinNumber;

	public float joyValue;

	public float topRot;
	public float bottomRot;

	public float mappedJoy;

	public float speed; 

	private Material Highlight;
	private Material DefaultColour;


	public GameObject ButtonToHighlight;

	private GameObject ArduinoScript;

    // Use this for initialization
    void Start()
    {
        rotmult = 0.8f;
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
		//For highlighting
		Highlight = GameObject.Find ("script").GetComponent<HighlightButtons> ().Highlight;
		DefaultColour = GameObject.Find ("script").GetComponent<HighlightButtons> ().Default01;
		//grab arduino object
		ArduinoScript = GameObject.Find ("Uniduino");
    }

    // Update is called once per frame

	void ConfigurePins()
	{
		//configure the Arduino pin to be analog for our joysticks
		arduino.pinMode(joyPinNumber, PinMode.ANALOG);

		arduino.reportAnalog(9, 1);  //up down
	}

    void Update()
	{
		//Check if arduino connected
		if (ArduinoScript.GetComponent<Arduino> ().Connected) {
			
			joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput

			mappedJoy = joyValue.Remap (1023, 0, -1, 1);
			if ((joyValue <= 250) || (joyValue >= 750)) {
				rotationX += mappedJoy * speed;
				rotationX = Mathf.Clamp (rotationX, bottomRot, topRot);
				transform.localEulerAngles = new Vector3 (rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
				ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
			} else {
				ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
			}

			//rotrate = mappedJoy*rotmult;
			//transform.Rotate(rotrate * Time.deltaTime,0.0f,0.0f);
		}
	}
}
