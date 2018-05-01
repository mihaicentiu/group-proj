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

	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;

	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;


    private float hcouchmovrate;
    private float hcouchmovmult;


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

		//Tell the Arduino to report any changes in the value of our joysticks2
		arduino.reportAnalog(joyPinNumber, 1);  //up down
	}

    // Update is called once per frame
    void Update ()
	{
		joyValue = arduino.analogRead (joyPinNumber); //joystick digital imput

		mappedJoy = joyValue.Remap (1023, 0, 1, -1); //changed imput for unity

		if ((joyValue <= 477) || (joyValue >= 569)) {
			
			hcouchmovrate = mappedJoy * hcouchmovmult;
			transform.Translate (0.0f, hcouchmovrate * Time.deltaTime, 0.0f);
		}
	}
}
