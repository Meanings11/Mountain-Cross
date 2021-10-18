/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRArm : MonoBehaviour {

	public AudioClip MowClip;
	public AudioClip CatPound;

	public AudioSource CatSource;
	public AudioSource PoundSource;

	//public Tempo beatget;
	Vector2 startPosition;
	Vector2 currentPos;

	public float beatCountdown = 2;

	public bool GoingBack = false;
	public bool Smash = false;

	private float GoingBackCooldown;
	public float GoingBackTime;
	public float GoingBackCooldownSet;
	public Vector2 raisePosition;
	public Vector2 smashPosition;

	public GameObject Beat;
	public Tempo rhythm;

	// Use this for initialization
	void Start () {

		Return();

		transform.position = new Vector2(3.87f, 1.92f);
		startPosition = transform.position;
		raisePosition = new Vector2 (4f, 2.43f);
		smashPosition = new Vector2 (1.5f, -2.5f);

		//beatCountdown = 1;

		beatCountdown = 2;

		CatSource.clip = MowClip;
		PoundSource.clip = CatPound;


		currentPos = startPosition;

		rhythm = Beat.GetComponent<Tempo>();

	}

	// Update is called once per frame
	void Update () {

		Return ();
		BeatTrigger ();

		if (rhythm.armSelect == 2) {
			if (rhythm.BeatTrigger == true) {
				if (beatCountdown >= 1) {
					//beatCountdown = 0;
					rhythm.BeatTrigger = false;
				} else if (beatCountdown <= 0) {
					beatCountdown = 2;
					rhythm.BeatTrigger = false;
					Smash = true;
				}
			}
		}
	}



	void BeatTrigger(){
		if ( && (beatCountdown == 1) && rhythm.armSelect == 2) {

			GoingBackCooldown = GoingBackCooldownSet;
			GoingBack = true;
			CatSource.PlayOneShot (MowClip, 0.25f);
			GoingBackCooldown = GoingBackCooldownSet;
		}



	}


	void Return(){

		if (GoingBack == true && beatCountdown == 1)
			transform.position = Vector2.Lerp (transform.position, raisePosition, Time.deltaTime * GoingBackTime);
		GoingBackCooldown -= Time.deltaTime;

		if (GoingBackCooldown <= 0f){
			GoingBack = false;
			//Smash = true;
			//GoingBackCooldown = GoingBackCooldownSet;
		}

		if ((beatCountdown == 2) && (Smash == true) && rhythm.armSelect == 2) {
			transform.position = smashPosition;
			Smash = false;
			CatSource.PlayOneShot (CatPound, 0.75f);
		}
		if (beatCountdown == 2 && Smash == false ){
			transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * 4);

		}


	}

}
*/