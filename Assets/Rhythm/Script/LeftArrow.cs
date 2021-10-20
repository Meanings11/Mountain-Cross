using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrow : MonoBehaviour {

	public GameObject Cat;
	public CatEntirely CatEntirelyCountdown;

	Vector2 startPosition;
	Vector2 currentPos;
	Vector2 endPos;

	public int GoingBackTime;

	// Use this for initialization
	void Start () {

		transform.position = new Vector2 (-30f, -1.68f);
		startPosition = new Vector2 (6.77f, -1.68f);
		//currentPos = transform.position;
		endPos = new Vector2 (-30f, -1.68f);
		CatEntirelyCountdown = Cat.GetComponent<CatEntirely>();

	}

	// Update is called once per frame
	void Update () {

		currentPos = transform.position;

		if (CatEntirelyCountdown.ShowLArrow == true) {

			transform.position = new Vector2 (-5.44f, -1.68f);

		}

		else{
			//Debug.Log ("ArrowShootLeft");
			transform.position = Vector2.Lerp (currentPos, endPos, Time.deltaTime * GoingBackTime);
		}

	}
}