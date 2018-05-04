using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdRotnCradle : MonoBehaviour {

	public Arduino arduino;
    private float rotrate;
    private float rotmult;
	private float rotationZ = 0f;

	public int joyPinNumber;

	public float joyValue;

	public float leftRot;
	public float rightRot;

	public float mappedJoy;
	public float speed;

	private Material Highlight;
	private Material DefaultColour;

	public GameObject ButtonToHighlight;

	private GameObject ArduinoScript;



    // Use this for initialization
    void Start()
    {
        rotmult = 1.0f;
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

		arduino.reportAnalog(10, 1);  //up down
	}

    // Update is called once per frame
    void Update()
	{
		//Check if arduino connected
		if ((ArduinoScript.GetComponent<Arduino> ().Connected) && (Time.timeScale == 1)) {
			joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput
			mappedJoy = joyValue.Remap (1023, 0, -1, 1);
			if ((joyValue <= 250) || (joyValue >= 750)) {
				rotationZ += mappedJoy * speed;
				rotationZ = Mathf.Clamp (rotationZ, leftRot, rightRot);
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ);




			}


			//if ((transform.rotation.z < rightRot) && (transform.rotation.z > leftRot)) {
			//rotrate = mappedJoy * rotmult;
			//transform.Rotate (0.0f, 0.0f, rotrate * Time.deltaTime);
		}
	}
}
