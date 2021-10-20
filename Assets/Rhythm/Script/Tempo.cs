using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	public bool SongStartupAgain = false;
	public int SongStartupAgainTimer;
	public bool stopVolume = false;

	public GameObject Cat;
	public CatEntirely CatEntirelyCountdown;
	public GameObject Knight;
	public KnightControls Mouse;
	public GameObject Background;
	public BackgroundFlash BackgroundColor;
	public GameObject Kidnapper;
	public GameObject leftArrow;
	public GameObject rightArrow;
	
	public AudioClip EricSong;
	public AudioClip HitAudio;

	public AudioSource EricSource;
	public AudioSource HitSource;

	//UI 
	// Timer
	public float gameTime = 30;
	public Text gameTimerText;
	public bool isGameOver;

	// Score Manage
	public Text scoreText;
	private int score;

	// Prompt
	public Text gameOverText;

	// Good Hit Effect
	public GameObject hitEffect;


	// Use this for initialization
	void Start () {

		youPressed = false;
		isGameOver = false;
		score = 0;

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

			
		if (sceneName == "RhythmScene") {
			bpm = 120f;
			bpmDivide = 60;
		}
	

	}
	
	void Update () {
		if (gameTime <= 0) 
        {
            GameOver();
        } else {
			MouseControls ();
			gameTime -= Time.unscaledDeltaTime;
			gameTimerText.text = gameTime.ToString("0");
		}

	}

	void FixedUpdate () {
		if (!isGameOver) {		
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
		}

	void MouseControls (){

		// if (Input.GetKeyDown (KeyCode.RightArrow) && (pressRight == true) && Mouse.isDead==false) {
		if(Input.GetMouseButtonDown(0)) {
			if (Input.mousePosition.x >= Screen.width/2 && (pressRight == true)) {
			
				if ((currentTime <= .25f || currentTime >= .75f)) {
					Debug.Log ("successright");
					scoreUp();
					YouPressed ();
					pressRight = false;

					// Hit effect
					HitSource.PlayOneShot (HitAudio, 0.75f);
					Instantiate(hitEffect,  new Vector3(0.0f, -4.5f, 0), Quaternion.identity);

				} else { 
					Debug.Log ("failRight");
					Mouse.isDead = true;
				}
			}

			if (Input.mousePosition.x <= Screen.width/2 && pressLeft == true) {
			
				if ((currentTime <= .25f || currentTime >= .75f)) {
					Debug.Log ("successleft");
					scoreUp();
					YouPressed ();
					pressLeft = false;

					// Hit effect
					HitSource.PlayOneShot (HitAudio, 0.75f);
					Instantiate(hitEffect,  new Vector3(0.0f, -4.5f, 0), Quaternion.identity);
				} else { 
					Mouse.isDead = true;
					Debug.Log ("failLeft");

				}
			}
		}

		// keyboard movement
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (pressLeft == true)) {
			if ((currentTime <= .25f || currentTime >= .75f)) {
				scoreUp();
				YouPressed ();
				pressLeft = false;

				// Hit effect
				HitSource.PlayOneShot (HitAudio, 0.75f);
				Instantiate(hitEffect,  new Vector3(0.0f, -4.5f, 0), Quaternion.identity);
			} else { 
				Mouse.isDead = true;
			}
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (pressRight == true)) {
            if ((currentTime <= .25f || currentTime >= .75f)) {
				scoreUp();
				YouPressed ();
				pressRight = false;

				// Hit effect
				HitSource.PlayOneShot (HitAudio, 0.75f);
				Instantiate(hitEffect,  new Vector3(0.0f, -4.5f, 0), Quaternion.identity);
			} else {
				Mouse.isDead = true;
			}
        }
	}

	void YouPressed(){

		Debug.Log ("YouPressed!");
		youPressed = true;
			
	}

	void GameOver() {
        EricSource.volume = 0f; 
		
        // Set score
		if (!isGameOver) {
			int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
			PlayerPrefs.SetInt("totalGameScore", currentGameScore+score);
		}

		isGameOver = true;

		// disable Arrow
		leftArrow.SetActive(false);
		rightArrow.SetActive(false);

		// End Game Anim
		Vector2 currentPos = Kidnapper.transform.position;
		Vector2 endPos = new Vector2 (currentPos.x, -30f);
		Kidnapper.transform.position = Vector2.Lerp (currentPos, endPos, Time.deltaTime * 2);

		// End Game Prompt
		gameOverText.gameObject.SetActive(true);
		gameOverText.text = (score >= 100) ? "Nice Dance!" : "Do Better!";


        // jump back to main board
        StartCoroutine(LoadEndScene());
    }
	
	void scoreUp() {
		score = score + 10;
		scoreText.text =  score.ToString();
	}

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");

    }
}

