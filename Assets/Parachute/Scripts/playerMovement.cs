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

    public Text GameoverText;
    public Text TotalPoint;
    public Text scoreText;
    public Text timer;

    private Vector3 pos;
    private float startTime = 0f;

    public bool firstHit = true;

    // Use this for initialization
    void Start () {
        startTime = Time.time;

        animator = GetComponent<Animator>();

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
            if (Time.time - startTime < 30) {
                timer.text = "00:00:" + (30 - Time.time).ToString("F0");
                
                moveUpdate();
                updateScore();
                // flyUp();
            } else {
                timer.text = "00:00:00";
                GameoverText.text = "Times Up!";
                EndGame();
            }
        }

        
    }

    private void updateScore()
    {
        score = Mathf.RoundToInt(Time.time - startTime) * 10;
        if (score == 0 || score == 00) {
            scoreText.text = "$0";
        } else {
            scoreText.text = "$" + string.Format("{0:0,0}", Int16.Parse(score.ToString()));
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

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        transform.position = new Vector3(pos.x + 1, pos.y + 1, pos.z);
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

        TotalPoint.text = "Earned " + scoreText.text + " in total";
        GameoverText.gameObject.SetActive(true);
        TotalPoint.gameObject.SetActive(true);

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

        CultureInfo provider = new CultureInfo("en-US");
        NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;

        decimal pointNumber = Decimal.Parse(scoreText.text, style, provider);
        int addedScore = Decimal.ToInt32(pointNumber);
        // Debug.Log(addedScore);
        PlayerPrefs.SetInt("totalGameScore", currentGameScore + addedScore);
                
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