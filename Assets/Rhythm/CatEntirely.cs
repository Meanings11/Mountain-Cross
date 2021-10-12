using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatEntirely : MonoBehaviour {

	public int []AttackSelect;

	public AudioClip MowClip;
	public AudioClip CatPound;

	public AudioSource CatSource;
	public AudioSource PoundSource;

	public bool currentlyattacking = false;

	//public Tempo beatget;
	public int rangeRange = 1;
	public int armSelect;

	public float beatCountdown = 2;

	public bool LSmash = false;
	public bool RSmash = false;

	public GameObject Beat;
	public Tempo rhythm;

	public GameObject Knight;
	public KnightControls Mouse;

	public bool ShowRArrow;
	public bool ShowLArrow;

	int currentPlaceInList;
	private bool useList;
	Animator anim;

	// Use this for initialization
	void Start () {

		Mouse = Knight.GetComponent<KnightControls>();
		anim = GetComponent<Animator>();

		//anim.SetBool("LRaise" , true);

		//ReturnLeft();
		//ReturnRight ();
		ShowRArrow = false;
		ShowLArrow = false;
		currentPlaceInList = 0;
		beatCountdown = 2;
		useList = true;
		CatSource.clip = MowClip;
		PoundSource.clip = CatPound;


		//currentPos = startPosition;

		rhythm = Beat.GetComponent<Tempo>();
		//rightarrow = RightArrow.GetComponent<RightArrow> ();


	}

	// Update is called once per frame
	void Update ()
	{
		
		//SmashLeft ();
		//SmashRight ();

		//BeatTriggerLeft ();
		//BeatTriggerRight ();

		/*if (rhythm.BeatTrigger == true) {
			if (beatCountdown == 2) {
				beatCountdown = 0;
				rhythm.BeatTrigger = false;
			} else if (beatCountdown == 0) {
				beatCountdown = 2;
				LSmash = true;
				RSmash = true;
				rhythm.BeatTrigger = false;

			}

		}*/
	
}
	public void TempoTrigger (){

		if (AttackSelect [currentPlaceInList] == 0 && useList == true) {
			//do nothing
			currentPlaceInList += 1;
			anim.SetTrigger ("BounceV2");
			Mouse.Bounce ();

		} else if (AttackSelect [currentPlaceInList] == 1) {
			//attack

			useList = false;
		}

		if (useList == false) {
			//Debug.Log ("attack");
			Attack ();
		}

			if (beatCountdown == 2) {
				beatCountdown = 0;
				rhythm.BeatTrigger = false;
			} else if (beatCountdown == 0) {
				beatCountdown = 2;
				LSmash = true;
				RSmash = true;
				rhythm.BeatTrigger = false;
				//Debug.Log (AttackSelect [currentPlaceInList]);
			
		}
	}

	void Attack (){
		rangeRange -= 1;
		//beatCountdown -= 1;
		if (rangeRange >= 1) {
			armSelect = (Random.Range (1, 3));

		} else if (rangeRange <= 1) {
			rangeRange = 2;
	
			//armSelect = (Random.Range (1, 3));
		}
		RaiseLeft ();
		RaiseRight ();
	}
		

	public void RaiseLeft(){

		if ((beatCountdown == 0) && (currentlyattacking == false) &&  armSelect == 1) {
			CatSource.PlayOneShot (MowClip, 0.25f);
			anim.SetBool ("L_Smash", false);
			anim.SetBool ("R_Smash", false);
			anim.SetBool ("LRaise", true);
			rhythm.pressRight = true;
			rhythm.pressLeft = false;
			//rhythm.youPressed = false;
			rhythm.unpress = false;
			ShowRArrow = true;
			currentlyattacking = true;
		}
		else if ((beatCountdown==2) && (currentlyattacking == true) && armSelect ==1) {
			anim.SetBool("L_Smash" , true);
			anim.SetBool ("LRaise", false);
			ShowRArrow = false;
			currentlyattacking = false;
			LSmash = false;
			RSmash = false;
			CatSource.PlayOneShot (CatPound, 0.75f);
		}
		if (beatCountdown == 2 && LSmash == false ){
			//transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * 4);
			anim.SetBool("Return" , true);
		}
		}


	public void RaiseRight(){
		
		if ((beatCountdown == 0) && (currentlyattacking == false) &&  armSelect == 2) {
			//Debug.Log ("RightRaise");
			CatSource.PlayOneShot (MowClip, 0.25f);
			anim.SetBool ("R_Smash", false);
			anim.SetBool ("L_Smash", false);	
			anim.SetBool ("R_Raise", true);
			rhythm.pressLeft = true;
			rhythm.pressRight = false;
			rhythm.unpress = false;
			//rhythm.youPressed = false;
			ShowLArrow = true;
			currentlyattacking = true;

		}
		else if ((beatCountdown == 2) && (currentlyattacking == true) && armSelect == 2){
			//Debug.Log("SMASH R");
			anim.SetBool("R_Smash" , true);
			anim.SetBool ("R_Raise", false);
			ShowLArrow = false;
			currentlyattacking = false;
			LSmash = false;
			RSmash = false;
			CatSource.PlayOneShot (CatPound, 0.75f);
		}
		if (beatCountdown == 2 && RSmash == false ){
			//transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * 4);
			anim.SetBool("Return" , true);
			//anim.SetBool ("R_Smash", false);
		}

		if (Mouse.isDead == true) {

			CatSource.volume = 0f;

		}

	}
		
	/*void SmashLeft(){
		
		if ((beatCountdown == 2) && (LSmash == true) && (currentlyattacking == true) && armSelect == 1){
			//transform.position = smashPosition;
			Debug.Log("SMASH L");
			anim.SetBool("L_Smash" , true);
			anim.SetBool ("LRaise", false);
			currentlyattacking = false;
			LSmash = false;
			RSmash = false;
			CatSource.PlayOneShot (CatPound, 0.75f);
		}
		if (beatCountdown == 2 && LSmash == false ){
			//transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * 4);
			anim.SetBool("Return" , true);
		}


	}

	void SmashRight(){
	
		if ((beatCountdown == 2) && (RSmash == true) && (currentlyattacking == true) && armSelect == 2 ){
			//transform.position = smashPosition;
			Debug.Log("SMASH R");
			anim.SetBool("R_Smash" , true);
			anim.SetBool ("R_Raise", false);
			currentlyattacking = false;
			LSmash = false;
			RSmash = false;
			CatSource.PlayOneShot (CatPound, 0.75f);
		}
		if (beatCountdown == 2 && RSmash == false ){
			//transform.position = Vector2.Lerp (transform.position, startPosition, Time.deltaTime * 4);
			anim.SetBool("Return" , true);
			//anim.SetBool ("R_Smash", false);
		}


	}*/

}
