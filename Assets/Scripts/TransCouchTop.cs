using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransCouchTop : MonoBehaviour {

    private float vcouchmovrate;
    private float vcouchmovmult;

    // Use this for initialization
    void Start()
    {
        vcouchmovmult = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        vcouchmovrate = Input.GetAxis("PadVert") * vcouchmovmult;

        if (transform.localPosition.y >= 0.74f)
        {
            vcouchmovrate = -0.01f;
        }

        if (transform.localPosition.y <= 0.37f)
        {
            vcouchmovrate = +0.01f;
        }

        transform.Translate(0.0f, vcouchmovrate * Time.deltaTime, 0.0f);
    }
}
