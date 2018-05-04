using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Uniduino;

public class Connection : MonoBehaviour {

	public Text ConnectionText;
	public Arduino arduino;
	private GameObject ArduinoScript;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
		ArduinoScript = GameObject.Find ("Uniduino");
	}
	
	// Update is called once per frame
	void Update () {
		if (ArduinoScript.GetComponent<Arduino> ().Connected) {
			ConnectionText.gameObject.SetActive (true);
		}
	}
}
