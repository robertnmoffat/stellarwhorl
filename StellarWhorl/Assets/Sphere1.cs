using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere1 : MonoBehaviour {

    GameObject inserted = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (inserted != null)
        {
            puke();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Deck")
        {
            inserted = other.gameObject;
        }
 
    }

    void OnTriggerExit()
    {
        if (inserted != null)
        {
            inserted = null;
        }
    }

    void puke()
    {
        Vector3 difference = inserted.gameObject.transform.position - transform.position;
		difference = difference.normalized;
        //difference = new Vector3(1/difference.x*50, 1/difference.y*50, 1/difference.z*50);
		difference = new Vector3(difference.x*50, difference.y*50, difference.z*50);
        //inserted.gameObject.transform.position = inserted.gameObject.transform.position + difference;
		Rigidbody rb = inserted.transform.parent.GetComponent<Rigidbody>();
        rb.AddForce(difference);
    } 
}
