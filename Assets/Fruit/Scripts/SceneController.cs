using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class SceneController : MonoBehaviour {

    [Header("Gameplay")]
    public int remainLives;
    public float timeRemaining = 30f;
    public ObjectSpawner fruitSpawner;
    public ObjectSpawner bombSpawner;

    [Header("Interfaces")]
    public LifeCounter lifeCounter;
    public Text GameoverText;
    public Text TotalPoint;
    public Text scoreText;
    public Text timer;

    public AudioSource timesUp;

    private int score = 0;

    void Start() {
        timesUp = GetComponent<AudioSource>();

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);

        lifeCounter.SetLives(remainLives);

        fruitSpawner.OnObjectSpawned += OnObjectSpawned;
        bombSpawner.OnObjectSpawned += OnObjectSpawned;
    }

    void Update()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
        } else {
            timer.text = "00:00:00";
            GameoverText.text = "Times Up!";
            timesUp.Play();
            
            EndGame();
        }
    }

    void OnObjectSpawned(CuttableObject obj) {
        obj.OnDestroyed += OnObjectDestroyed;
    }

    void OnObjectDestroyed(bool harmful) {
        if (!harmful) {
            score += 10;
            scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
        } else {
            LoseLife();
        }
    }

    void LoseLife() {
        remainLives--;

        lifeCounter.LoseLife();

        // gameover
        if (remainLives <= 0) {
            EndGame();
        }
    }

    void EndGame() {
        scoreText.gameObject.SetActive(false);

        // Get total score
        TotalPoint.text = "Earned " + scoreText.text + " in total";
        GameoverText.gameObject.SetActive(true);
        TotalPoint.gameObject.SetActive(true);

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        PlayerPrefs.SetInt("totalGameScore", currentGameScore + score);

        // load back to main board
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");
    }
}