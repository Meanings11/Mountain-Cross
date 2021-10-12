using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatClawsReturn : MonoBehaviour {

	public Vector2 startPos;
	public bool goAway;


	// Use this for initialization
	void Start () {

		goAway = false;
		startPos = transform.position;

		transform.position = new Vector2 (214, 354);

	}

	// Update is called once per frame
	void Update () {

		if (goAway == true){
			transform.position = startPos;
		}
		else{
			transform.position = new Vector2 (214, 354);
		}
	}
}
