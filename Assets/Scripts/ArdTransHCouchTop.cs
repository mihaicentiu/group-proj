using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArdTransHCouchTop : MonoBehaviour {
	[Header("Arduino Variables")]

	//we need to declare the Arduino as a variable
	public Arduino arduino;

	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;
	public int joyPinNumber2;

	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;
	public float joyValue2;

	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;
	public float mappedJoy2;

	public float leftEdge;
	public float rightEdge;


	public float topEdge;
	public float bottomEdge;


    private float hcouchmovrate;
    private float hcouchmovmult;
	private float hcouchmovrate2;


    // Use this for initialization
    void Start()
    {
        hcouchmovmult = 0.1f;
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
    }

	void ConfigurePins()
	{
		//configure the Arduino pin to be analog for our joysticks
		arduino.pinMode(joyPinNumber, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber2, PinMode.ANALOG);

		//Tell the Arduino to report any changes in the value of our joysticks2
		arduino.reportAnalog(joyPinNumber, 1);  //up down
		arduino.reportAnalog(13, 1);  //up down
		arduino.reportAnalog(15, 1); 
	}

    // Update is called once per frame
    void Update ()
	{
		joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput
		joyValue2 = arduino.analogRead (joyPinNumber2); //joystick digital imput

		mappedJoy = joyValue.Remap (1023, 0, 1, -1); //changed imput for unity
		mappedJoy2 = joyValue2.Remap (1023, 0, 1, -1); //changed imput for unity


		if ((joyValue <= 477) || (joyValue >= 569) && (transform.localPosition.x >= bottomEdge && transform.localPosition.x <= topEdge)) {
			hcouchmovrate = mappedJoy * hcouchmovmult;
			transform.Translate (0.0f, hcouchmovrate * Time.deltaTime, 0.0f);
		}

		if ((joyValue2 <= 477) || (joyValue2 >= 569) && (transform.localPosition.z >= leftEdge && transform.localPosition.z <= rightEdge)) {
			hcouchmovrate2 = mappedJoy2 * hcouchmovmult;
			transform.Translate (hcouchmovrate2 * Time.deltaTime,0.0f, 0.0f);
		}
	}
}
