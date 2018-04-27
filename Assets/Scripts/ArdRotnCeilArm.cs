using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;



//we need to declare the Arduino as a variable


public class ArdRotnCeilArm : MonoBehaviour {
	[Header("Arduino Variables")]

	public Arduino arduino;

	public int rotateLeftPinNumber;
	public int rotateRightPinNumber;

	public float leftRotateEdge;
	public float rightRotateEdge;

    private float rotrate;
    private float rotmult;
	private float rotationY = 0f;

	public float speed;

	public Material Highlight;
	public Material DefaultColour;

	public GameObject ButtonToHighlight;

    // Use this for initialization
    void Start()
    {
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
        rotmult = 1.0f;
		//For highlighting
		Highlight = GameObject.Find ("script").GetComponent<HighlightButtons> ().Highlight;
		DefaultColour = GameObject.Find ("script").GetComponent<HighlightButtons> ().Default01;
    }

	void ConfigurePins()
	{
		arduino.pinMode(rotateLeftPinNumber, PinMode.INPUT);
		arduino.pinMode (rotateRightPinNumber, PinMode.INPUT);
		arduino.reportDigital((byte)(rotateLeftPinNumber / 9), 1);
		arduino.reportDigital((byte)(rotateRightPinNumber / 12), 1);

	}

    // Update is called once per frame
    void FixedUpdate()
	{



			if (arduino.digitalRead (rotateRightPinNumber) == 1) {
			rotationY += speed;
			rotationY = Mathf.Clamp (rotationY,leftRotateEdge, rightRotateEdge);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,rotationY, transform.localEulerAngles.z);
			ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
		}
		else {
			ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
		}	
			//rotrate = -rotmult;
				//transform.Rotate (0.0f, rotrate * Time.deltaTime, 0.0f);

		if (arduino.digitalRead (rotateLeftPinNumber) == 1) {
			rotationY += -speed;
			rotationY = Mathf.Clamp (rotationY, leftRotateEdge, rightRotateEdge);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,rotationY, transform.localEulerAngles.z);
			ButtonToHighlight.GetComponent<Renderer> ().material = Highlight;
		}
		else {
			ButtonToHighlight.GetComponent<Renderer> ().material = DefaultColour;
		}
			//rotrate = -rotmult;
			//transform.Rotate (0.0f, rotrate * Time.deltaTime, 0.0f);

			//Debug.Log(arduino.digitalRead (rotateLeftPinNumber));

			//if (arduino.digitalRead (rotateLeftPinNumber) == 1) {
				//rotrate = rotmult;
				//transform.Rotate (0.0f, rotrate * Time.deltaTime, 0.0f);
		}
}