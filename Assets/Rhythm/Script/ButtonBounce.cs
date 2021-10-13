using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBounce : MonoBehaviour {

	public Vector2 returnScale;

	// Use this for initialization
	void Start () {
		returnScale = new Vector2 (.11f, .11f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector2.Lerp (transform.localScale, returnScale, Time.deltaTime * 5);
	}

	public void TempoTrigger(){
		transform.localScale = new Vector2 (.14f, .14f);
	}
}
