using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {
	public float thrustForce = 10;
	public float rotationForce = 2.0f;
	public GameObject board;
	public bool isGrounded = true;
	public float maxVelocity = 10.0f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = board.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position = board.transform.position;
		rotateTowardsSlower (board.transform, transform);
		//transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, board.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		if (Input.GetKey (KeyCode.UpArrow)&&getHighestVectorValue(rb.velocity)<=maxVelocity) {
			rb.AddForce (board.transform.forward * thrustForce);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			board.transform.Rotate (0.0f, -rotationForce, 0.0f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			board.transform.Rotate (0.0f, rotationForce, 0.0f);
		}
	}

	public void rotateTowardsSlower(Transform trans1, Transform trans2){	
		float diff = getAngleShortestDiff (trans2.rotation.eulerAngles.y, trans1.rotation.eulerAngles.y);	
		//float diff = trans1.rotation.eulerAngles.y - trans2.rotation.eulerAngles.y;
		diff = diff / 50;
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y+diff, transform.rotation.eulerAngles.z);
	}

	public float getAngleShortestDiff(float angle1, float angle2){
		float plusAngle=0;
		float minusAngle=0;

		if (angle1 < angle2) {
			minusAngle += angle1;
			minusAngle += 360 - angle2;

			plusAngle = angle2 - angle1;
		} else {
			minusAngle = angle1 - angle2;

			plusAngle += 360 - angle1;
			plusAngle += angle2;
		}

		if (plusAngle < minusAngle)
			return plusAngle;

		return -minusAngle;
	}

	public float getHighestVectorValue(Vector3 vec){
		float highest = 0;
		if (vec.x > highest)
			highest = vec.x;
		if (vec.y > highest)
			highest = vec.y;
		if (vec.z > highest)
			highest = vec.z;

		return highest;
	}		
}
