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

    // Use this for initialization
    void Start()
    {
        intensmovmult = 0.1f;
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
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

		joyValue = arduino.analogRead(joyPinNumber); //joystick digital imput

		mappedJoy = joyValue.Remap (1023, 0, -1, 1);

		intensmovrate = Input.GetAxis("VerticalL") * intensmovmult;
        
		intensmovrate = mappedJoy * intensmovmult;
	
        if (transform.localPosition.y >= 1.17f)
        {
            intensmovrate = -0.01f;
        }

        if (transform.localPosition.y <= 0.65f)
        {
            intensmovrate = +0.01f;
        }

        transform.Translate(0.0f, intensmovrate * Time.deltaTime, 0.0f);
    }
}