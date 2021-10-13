﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tempo : MonoBehaviour {

	public bool pressRight;
	public bool pressLeft;

	public bool youPressed;

	public bool unpress;

	public bool beatPass;
	public bool beatFail;
	public int rangeRange = 1;
	public int armSelect;
	public float currentTime;
	public float StartTempo;
	public bool BeatTrigger = false;
	float bpm = 120f;
	float bpmDivide;

	// Score Manage

	public bool ScoreUp = false;
	
	public bool ScoreDown = false;

	public bool SongStartupAgain = false;
	public int SongStartupAgainTimer;
	public bool stopVolume = false;

	public GameObject Cat;
	public CatEntirely CatEntirelyCountdown;
	public GameObject Knight;
	public KnightControls Mouse;
	public GameObject Background;
	public BackgroundFlash BackgroundColor;

	public AudioClip EricSong;
	public AudioClip HitAudio;

	public AudioSource EricSource;
	public AudioSource HitSource;


	// Use this for initialization
	void Start () {

		youPressed = false;

		EricSource.clip = EricSong;
		HitSource.clip = HitAudio;

		SongStartupAgainTimer = 200;

		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;

		currentTime = 0f;
		CatEntirelyCountdown = Cat.GetComponent<CatEntirely>();
		BackgroundColor = Background.GetComponent<BackgroundFlash>();
		Mouse = Knight.GetComponent<KnightControls>();

		EricSource.PlayOneShot (EricSong, 0.697f);

		if (sceneName == "Scene1") {
			bpm = 120f;
			bpmDivide = 40;
		}
			
		if (sceneName == "EasyMode") {
			bpm = 120f;
			bpmDivide = 60;
		}
	

	}
	
	void Update () {
		MouseControls ();

	}

	void FixedUpdate () {
		currentTime += Time.deltaTime * (bpm / bpmDivide);

		if(currentTime >= 1f) {
			currentTime -= 1f;
			//CatEntirelyCountdown.beatCountdown -= 1;
			BeatTrigger = true;
			CatEntirelyCountdown.TempoTrigger();
			BackgroundColor.TempoTrigger();
			//unpress = false;
			//rangeRange -= 1;
			//if (rangeRange >= 1) {
				//armSelect = (Random.Range (1, 3));
				//CatEntirelyCountdown.BeatTriggerLeft ();
				//CatEntirelyCountdown.BeatTriggerRight ();
			//} else if (rangeRange <= 1) {
			//	rangeRange = 2;
				//armSelect = (Random.Range (1, 3));
			//}

		}

		// if ((currentTime > .7f) && unpress==false && (CatEntirelyCountdown.beatCountdown == 0) && (Mouse.isDead == false)) {
		if ((currentTime > .7f) && unpress==false && (CatEntirelyCountdown.beatCountdown == 0)) {
		
			Debug.Log ("unpress");
			youPressed = false;
			unpress = true;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) && pressLeft == true) {
				Debug.Log ("failWrongRight");
				Mouse.isDead = true;

			}

		else if (Input.GetKeyDown (KeyCode.LeftArrow) && pressRight == true) {
			Mouse.isDead = true;
			Debug.Log ("failWrongLeft");
			}

		//if (((pressLeft || pressRight) == true) && (youPressed == false) && (currentTime > .25f) && (currentTime < .35f) && (CatEntirelyCountdown.beatCountdown == 0) && (Mouse.isDead==false)) {
		else if (((pressLeft || pressRight) == true) && (youPressed == false) && (currentTime > .25f) && (currentTime < .35f) && (CatEntirelyCountdown.beatCountdown == 0)) {
		
			Mouse.isDead = true;
			Debug.Log ("failnopress");
		}

		// if (Mouse.isDead == true){
		// 	EricSource.volume = 0f; 
		// }
		if (SongStartupAgain == true && stopVolume == false) {
			SongStartupAgainTimer -= 1;
			if (SongStartupAgainTimer <= 0){
				EricSource.volume += .3f;
				//if(EricSource.volume >= .7){
					//Debug.Log ("STOPTHEMUSIC");
					//stopVolume = true;
				//}
			}


		}
		}

	void MouseControls (){

		// if (Input.GetKeyDown (KeyCode.RightArrow) && (pressRight == true) && Mouse.isDead==false) {
		if (Input.GetKeyDown (KeyCode.RightArrow) && (pressRight == true)) {
		
			if ((currentTime <= .25f || currentTime >= .75f)) {
				Debug.Log ("successright");
				ScoreUp = true;
				YouPressed ();
				pressRight = false;
				HitSource.PlayOneShot (HitAudio, 0.75f);

			} else { 
				Debug.Log ("failRight");
				ScoreDown = true;
				Mouse.isDead = true;
			}
		}
		// if (Input.GetKeyDown (KeyCode.LeftArrow) && pressLeft == true && Mouse.isDead==false) {
		if (Input.GetKeyDown (KeyCode.LeftArrow) && pressLeft == true) {
		
			if ((currentTime <= .25f || currentTime >= .75f)) {
				Debug.Log ("successleft");
				ScoreUp = true;
				YouPressed ();
				pressLeft = false;
				HitSource.PlayOneShot (HitAudio, 0.75f);
			} else { 
				Mouse.isDead = true;
				ScoreDown = true;
				Debug.Log ("failLeft");

			}
		}
	}

	void YouPressed(){

		Debug.Log ("YouPressed!");
		youPressed = true;
			
	}
	}

