using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotnCeilArm : MonoBehaviour {

    private float rotrate;
    private float rotmult;


    // Use this for initialization
    void Start()
    {
        rotmult = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotrate = Input.GetAxis("HorizontalL") * rotmult;
        transform.Rotate(0.0f, rotrate * Time.deltaTime, 0.0f);
    }
}
