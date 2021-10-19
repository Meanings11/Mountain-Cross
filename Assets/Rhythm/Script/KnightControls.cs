using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControls : MonoBehaviour {

	public AudioClip DodgeClip;

	public AudioSource MouseSource;

	public GameObject Beat;
	public Tempo rhythm;

	float t;
	Vector2 startPosition;
	Vector2 currentPos;
	public bool GoingBack = false;
	private float GoingBackCooldown = 0f;
	public float GoingBackTime;
	public float GoingBackCooldownSet;

	public bool isDead = false;

	Animator anim;

	//public KnightControls layerorder;

	// Use this for initialization
	void Start () {
		rhythm = Beat.GetComponent<Tempo>();

		anim = GetComponent<Animator>();
		isDead = false;
		//transform.position = new Vector2(0f, -3.33f);
		startPosition = transform.position;
		currentPos = startPosition;
		MouseSource.clip = DodgeClip;
		//transform.localScale = new Vector2 (.4f, .4f);
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
		anim.SetBool ("Dead", false);
		anim.SetBool ("Dodge_Left", false);
		anim.SetBool ("Dodge_Right", false);
		anim.SetBool ("Return", false);

	}
	
	// Update is called once per frame
	void Update () {

		Return ();
		var currentPos = transform.position;

		// if (Input.GetKeyDown (KeyCode.LeftArrow) && (GoingBackCooldown <= 0f) && isDead == false) {
		if(Input.GetMouseButtonDown(0) && GoingBackCooldown <= 0f) {

			if (Input.mousePosition.x > Screen.width/2) {
			
				anim.SetBool ("Dodge_Right", true);
				Debug.LogFormat("currentTime = {0}", rhythm.currentTime);
				GoingBackCooldown = GoingBackCooldownSet;
				GoingBack = true;
				MouseSource.PlayOneShot (DodgeClip, 1f);

			} else {
			
				Debug.LogFormat("currentTime = {0}", rhythm.currentTime);
				anim.SetBool ("Dodge_Left", true);
				GoingBackCooldown = GoingBackCooldownSet;
				GoingBack = true;
				MouseSource.PlayOneShot (DodgeClip, 1f);
			}
		}

		if (isDead == true) {

			//anim.SetBool ("L_Smash", false);
			// anim.SetBool ("Dead", true);
			//gameObject.GetComponent<SpriteRenderer>().sortingOrder = -100;
			//transform.position = new Vector2 (0f, -4f);
			//transform.localScale = new Vector2 (.6f, .1f);

		}
	}
	void Return () {

		if (GoingBack == true){

			//transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * GoingBackTime);
			GoingBackCooldown -= Time.deltaTime;
			
		}
		if (GoingBackCooldown < 0f ){
			GoingBack = false;
			anim.SetBool ("Return", true);
			anim.SetBool ("Dodge_Left", false);
			anim.SetBool ("Dodge_Right", false);
		}
	}

	public void Bounce () {
		anim.SetTrigger ("Bounce");
	}
}

