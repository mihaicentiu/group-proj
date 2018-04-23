using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransHCouchMid : MonoBehaviour {

    private float hcouchmidmovrate;
    private float hcouchmidmovmult;

    // Use this for initialization
    void Start()
    {
        hcouchmidmovmult = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        hcouchmidmovrate = -Input.GetAxis("PadHoriz") * hcouchmidmovmult;
        transform.Translate( hcouchmidmovrate * Time.deltaTime, 0.0f, 0.0f);
    }
}
