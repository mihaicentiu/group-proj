using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransIntens : MonoBehaviour {

    private float intensmovrate;
    private float intensmovmult;

    // Use this for initialization
    void Start()
    {
        intensmovmult = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        intensmovrate = Input.GetAxis("VerticalL") * intensmovmult;

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
