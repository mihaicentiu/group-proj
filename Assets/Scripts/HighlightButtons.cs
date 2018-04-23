using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtons : MonoBehaviour {

	//Left Stick
	public GameObject LArmHorizButton;
	public GameObject LArmUpButton;
	public GameObject IntensifierUpDownButton;

	//Right Stick
	public GameObject CArmClockButton;  //left 
	public GameObject CArmAntiClockButton; //right

	//Dpad
	public GameObject TableUpButton;
	public GameObject TableDownButton;
	public GameObject TableForwardButton;
	public GameObject TableBackButton;

	//TriggerL
	public GameObject CeilingBoxBack;
	//TriggerR
	public GameObject CeilingBoxForward;

	public Material Highlight;
	public Material Default01;
	public Material Default07;
	public Material Default08;


	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {


		if ((Input.GetAxis ("VerticalL") > 0.2) || (Input.GetAxis ("VerticalL") < -0.2) )  {
			IntensifierUpDownButton.GetComponent<Renderer> ().material = Highlight;
		}
		else if ((Input.GetAxis ("VerticalL") < 0.2) && (Input.GetAxis ("VerticalL") > -0.2) ) {
			IntensifierUpDownButton.GetComponent<Renderer> ().material = Default01;
		}

		
	}
}
