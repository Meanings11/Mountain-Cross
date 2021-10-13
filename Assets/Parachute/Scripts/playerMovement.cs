using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class playerMovement : MonoBehaviour {
    private float speed = 10;
    private int score = 0;

    public Animator animator;

    public GameObject player;
    public GameObject hitEffect;
    public GameObject boxGenerator;
    public GameObject cloudGenerator;

    public AudioSource flySound;
    public AudioClip timesUp;

    public Text GameoverText;
    public Text TotalPoint;
    public Text scoreText;
    public Text timer;

    private Vector3 pos;

    private bool firstHit = true;

    private float timeRemaining = 30f;

    // Use this for initialization
    void Start () {
        // initialize animator
        animator = GetComponent<Animator>();

        // initialize suound source
        flySound = GetComponent<AudioSource> ();

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        float ax = Input.GetAxis("Horizontal");
        float ay = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(ax, ay) * Time.deltaTime * 0.1f);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerFlyAnimation")) {
            // end the game after 30s
            
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
                moveUpdate();
                updateScore(30 - timeRemaining);
                // flyUp();
            } else {
                timer.text = "00:00:00";
                GameoverText.text = "Times Up!";

                if (firstHit == true) {
                    flySound.PlayOneShot(timesUp);
                }
                firstHit = false;
                
                EndGame();
            }
        }
    }

    private void updateScore(float timeScore)
    {
        score = Mathf.RoundToInt(timeScore / 2) * 10;
        Debug.Log(score);
        if (score == 0 || score == 00) {
            scoreText.text = "$0";
        } else {
            scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
        }
    }

    private void flyUp()
    {
        if (transform.position.y < 0)
        {
            transform.position = Vector3.MoveTowards(player.transform.position,
                new Vector3(transform.position.x, 0, transform.position.z), 
                Time.deltaTime * speed);
        }
    }

    void moveUpdate()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            touchPosition.z = player.transform.position.z - Camera.main.transform.position.z;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // touchPosition.y = player.transform.position.y;
            player.transform.position = Vector3.MoveTowards(player.transform.position, touchPosition, Time.deltaTime * speed);
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "box") {
            // destroy the hitted box
            Destroy(col.gameObject);

            if (firstHit == true) {
                animator.SetTrigger("Dead");
                Instantiate(hitEffect, col.transform.position, Quaternion.identity);

                EndGame();
            }

            firstHit = false;
        }
    }

    void EndGame() {
        // no longer generate new boxes
        Destroy(boxGenerator);

        // Get total score
        TotalPoint.text = "Earned " + scoreText.text + " in total";
        GameoverText.gameObject.SetActive(true);
        TotalPoint.gameObject.SetActive(true);

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

        // CultureInfo provider = new CultureInfo("en-US");
        // NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;

        // decimal pointNumber = Decimal.Parse(scoreText.text, style, provider);
        // int addedScore = Decimal.ToInt32(pointNumber);
        // Debug.Log(score);
        PlayerPrefs.SetInt("totalGameScore", currentGameScore + score);
                
        // End scene
        // SceneManager.LoadScene("BoardScene");
        StartCoroutine(LoadEndScene());
        // Invoke("LoadEndScene", 2f);
    }

    // void LoadEndScene()
    // {
    //     SceneManager.LoadScene("BoardScene");
    // }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");
    }
}