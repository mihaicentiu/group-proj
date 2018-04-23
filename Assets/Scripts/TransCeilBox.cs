using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransCeilBox : MonoBehaviour {

    private float movrateL;
    private float movrateR;
    private float movrate;
    private float movmult;

    // Use this for initialization
    void Start()
    {
        movmult = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        movrateL = Input.GetAxis("TriggerL") * movmult;
        movrateR = Input.GetAxis("TriggerR") * movmult;
        movrate = movrateR - movrateL;
        transform.Translate(0.0f, 0.0f, movrate * Time.deltaTime);
		if (transform.position.z <= -4)
		{	
			transform.position = new Vector3 (transform.position.x, transform.position.y, -4);
		}
		if (transform.position.z >= 1)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y, 1);
		}
	}
}
