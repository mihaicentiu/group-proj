using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransHCouchTop : MonoBehaviour {

    private float hcouchmovrate;
    private float hcouchmovmult;

    // Use this for initialization
    void Start()
    {
        hcouchmovmult = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        hcouchmovrate = Input.GetAxis("PadHoriz") * hcouchmovmult;
        transform.Translate(0.0f, hcouchmovrate * Time.deltaTime, 0.0f);
    }
}
