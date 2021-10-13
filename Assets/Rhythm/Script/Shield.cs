using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	// Use this for initialization
	float t;
	Vector2 startPosition;
	Vector2 currentPos;
	public bool GoingBack = false;
	private float GoingBackCooldown = 0;
	public float GoingBackTime;
	public float GoingBackCooldownSet;

	GameObject shield;

	// Use this for initialization
	void Start () {

		shield.GetComponent<Renderer>().enabled = false;
		//shield.SetActive (false);
		startPosition = transform.position;
		currentPos = startPosition;


	}

	// Update is called once per frame
	void Update () {

		Return ();
		var currentPos = transform.position;

		if (Input.GetKeyDown (KeyCode.RightArrow) && (GoingBackCooldown <= 0f)) {

			GoingBackCooldown = GoingBackCooldownSet;
			GoingBack = true;
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) && (GoingBackCooldown <= 0f)) {

			GoingBackCooldown = GoingBackCooldownSet;
			GoingBack = true;


		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && (GoingBackCooldown <= 0f)){ 
			//shield.SetActive (true);
			GoingBackCooldown = .5f;
			GoingBack = true;


		}
	}
	void Return () {

		if (GoingBack == true){

			GoingBackCooldown -= Time.deltaTime;

		}
		if (GoingBackCooldown < 0f ){
			//shield.SetActive (false);
			GoingBack = false;
		}
	}
}
