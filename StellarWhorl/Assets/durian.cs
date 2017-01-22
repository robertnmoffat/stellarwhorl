 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class durian : MonoBehaviour {

    public GameObject player;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 difference = player.transform.position - transform.position;
        difference = difference.normalized;
        rb.AddForce(difference * 420);
    }

}
