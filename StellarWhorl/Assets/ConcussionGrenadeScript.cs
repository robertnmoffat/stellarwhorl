using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcussionGrenadeScript : MonoBehaviour {
	public Transform concussionOut;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		Instantiate (concussionOut, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
