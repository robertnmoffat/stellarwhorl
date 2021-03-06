﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour {
	public float thrustForce = 10;
	public float rotationForce = 4.0f;
	public GameObject board;
	public bool isGrounded = true;
	public float maxVelocity = 10.0f;
	public GameObject cameraObject;
	public Text screenText;
	public Text wetScreenText;
	public Text greenText;
	public Text wetScore;
	public Text lapRecordText;
	public Text speedScore;
	public Text combinedScoreText;
	public int lapRecord=200;
	public BoardScript boardScript;
	public int airTime = 0;
	public int speedTime = 0;
	public int greenPower = 0;

	float textRotate = 0.0f;
	public int CONCUSSION = 0;

	int shotHeld=0;
	Rigidbody rb;
	Camera cam;

	// Use this for initialization
	void Start () {
		rb = board.GetComponent<Rigidbody> ();
		cam = cameraObject.GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position = board.transform.position;
		rotateTowardsSlower (board.transform, transform);
		//transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, board.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		float velocity = getHighestVectorValue (rb.velocity);

		if (greenPower > 20) {
			greenText.enabled = true;
			greenText.transform.Rotate (new Vector3 (0, 0, greenPower));
		} else {
			greenText.enabled = false;
		}

		if (!boardScript.isCollided) {
			airTime++;
			wetScore.enabled = true;
			wetScore.text = "+"+airTime*airTime;
			if(airTime*airTime>lapRecord){
				lapRecordText.enabled = true;
				lapRecord = airTime * airTime;
			}
			if (airTime > 10)
				wetScreenText.enabled = true;
			wetScreenText.transform.Rotate (new Vector3(0,0,airTime));
		} else {
			lapRecordText.enabled = false;
			wetScore.enabled = false;
			greenPower = 0;
			airTime = 0;
			wetScreenText.enabled = false;
		}

		if (velocity >= maxVelocity + 2) {
			screenText.enabled = true;
			speedTime++;
			speedScore.enabled = true;
			speedScore.text = "+"+speedTime*speedTime;

			if (airTime > 10)
				greenPower++;

			float speedDiff = velocity - maxVelocity;
			screenText.transform.Rotate (new Vector3(0,0,speedDiff*2));
			Vector3 scale = board.transform.localScale;
			//board.transform.localScale = new Vector3 (scale.x, scale.y, 1+speedDiff);

			if (60 + speedDiff*2 < 179)
				cam.fieldOfView = 60 + speedDiff*4;
			else
				cam.fieldOfView = 179;
		} else {
			speedScore.enabled = false;
			speedTime = 0;
			screenText.enabled = false;
			cam.fieldOfView = 60;
		}	

		if (speedTime > 0 && airTime > 0) {
			combinedScoreText.enabled = true;
			combinedScoreText.text = "+"+speedTime * speedTime * airTime * airTime+"!!!";
		} else {
			combinedScoreText.enabled = false;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {			
			Vector3 tempForwardForce = board.transform.forward * thrustForce;
			float tempx, tempy, tempz;
			if (Mathf.Abs(rb.velocity.x) > maxVelocity)
				tempx = 0;
			else
				tempx = tempForwardForce.x;

			if (Mathf.Abs(rb.velocity.y) > maxVelocity)
				tempy = 0;
			else
				tempy = tempForwardForce.y;

			if (Mathf.Abs(rb.velocity.z) > maxVelocity)
				tempz = 0;
			else
				tempz = tempForwardForce.z;

			rb.AddForce (new Vector3(tempx, tempy, tempz));

			if (velocity > maxVelocity){
				float speedDiff = velocity - maxVelocity;
				//rb.AddForce (board.transform.forward * (thrustForce-speedDiff));
			}
		}

		//Carving crap
		float totalVel = rb.velocity.x + rb.velocity.z;
		float percentx = rb.velocity.x/totalVel;
		float percentz = rb.velocity.z/totalVel;

		float portionx = Mathf.Sin (transform.rotation.eulerAngles.y)*180/Mathf.PI;
		float portiony = Mathf.Cos (transform.rotation.eulerAngles.y)*180/Mathf.PI;

		print (portionx + portiony);

		

			

		if (Input.GetKey (KeyCode.LeftArrow)) {
			board.transform.Rotate (0.0f, -rotationForce, 0.0f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			board.transform.Rotate (0.0f, rotationForce, 0.0f);
		}

		if (Input.GetKey (KeyCode.Space)) {
			shotHeld+=10;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			Vector3 explodeDirection = -rb.velocity.normalized;

			//GameObject grenade = Instantiate (Resources.Load ("concussionOut"), 
			//	new Vector3 (transform.position.x, transform.position.y+0.1f, board.transform.position.z),
			//	Quaternion.identity) as GameObject;
			
			GameObject grenade = Instantiate (Resources.Load ("concussionOut"), 
				new Vector3 (transform.position.x-transform.forward.x*0.5f, transform.position.y+0.1f-transform.forward.y*0.5f, board.transform.position.z-transform.forward.z*0.5f),
				Quaternion.identity) as GameObject;

			//Rigidbody grb = grenade.GetComponent<Rigidbody> ();
			//grb.AddForce (transform.forward * shotHeld);
			//grb.AddForce (transform.up * -300);
			//grb.AddForce (transform.up * shotHeld/2);


			
			//Instantiate (concussionOut, transform.position, Quaternion.identity);

			shotHeld = 0;
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)&&boardScript.isCollided) {
			rb.AddForce (board.transform.up*500);
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
		if (Mathf.Abs(vec.x) > highest)
			highest = Mathf.Abs(vec.x);
		if (Mathf.Abs(vec.y) > highest)
			highest = Mathf.Abs(vec.y);
		if (Mathf.Abs(vec.z) > highest)
			highest = Mathf.Abs(vec.z);

		return highest;
	}	

	public void shootGrenade(int grenadeType){
		
	}
}
