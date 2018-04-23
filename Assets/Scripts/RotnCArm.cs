using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotnCArm : MonoBehaviour {

    private float rotrate;
    private float rotmult;
	private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rotmult = 20.0f;
		rb = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        rotrate = Input.GetAxis("VerticalR") * rotmult;
        transform.Rotate(rotrate * Time.deltaTime,0.0f,0.0f);
	
    }
		
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "CouchTop") {
			rb.velocity = new Vector3 (0, 0, 0);
			rb.drag = 1000000;
			rb.angularDrag = 10000000;

		}
		 if (col.gameObject.name == "CouchTop" || col.gameObject.name == "CouchBase" || col.gameObject.name == "CouchBellows" || col.gameObject.name == "CouchEnclosure") {
			transform.position = new Vector3 (0, transform.position.y, transform.position.z);
		}

	}

}
