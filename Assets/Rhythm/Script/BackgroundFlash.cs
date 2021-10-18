using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlash : MonoBehaviour {


	public Color lerpedColor;
	public Color startColor;
	public Color currentColor;
	public int flashcountdown = 2;

	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		currentColor = rend.material.color;
	}

	// Update is called once per frame
	void Update () {
		currentColor = rend.material.color;
		rend.material.color = Color.Lerp (currentColor, startColor, Time.deltaTime * 18);
	}

	public void TempoTrigger(){

		flashcountdown -= 1;

		if (flashcountdown == 0) {
			Debug.Log ("BackgroundFlash");
			rend.material.color = lerpedColor;
			flashcountdown = 2;
		}
	}

}