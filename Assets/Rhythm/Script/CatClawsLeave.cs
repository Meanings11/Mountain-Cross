using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatClawsLeave : MonoBehaviour {

	public Vector2 startPos;
	public bool goAway;


	// Use this for initialization
	void Start () {

		goAway = false;
		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (goAway == true){
			transform.position = new Vector2 (132, 134);
	}
		else{
			transform.position = startPos;
		}
}
	public void OnMouseDown(){

		//goAway = true;



	}
}
