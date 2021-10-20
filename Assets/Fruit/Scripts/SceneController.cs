using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    [Header("Gameplay")]
    public int remainLives;
    public ObjectSpawner fruitSpawner;
    public ObjectSpawner bombSpawner;

    [Header("Interfaces")]
    public Text scoreText;
    public LifeCounter lifeCounter;
    public GameObject gameOverGroup;

    private int score = 0;
    public int Score {
        get { return score; }
        set {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }
    void Start() {
        gameOverGroup.SetActive(false);
        lifeCounter.SetLives(remainLives);

        fruitSpawner.OnObjectSpawned += OnObjectSpawned;
        bombSpawner.OnObjectSpawned += OnObjectSpawned;

        Score = 0;
    }

    void Update()
    {
        
    }

    private void OnObjectSpawned(CuttableObject obj) {
        obj.OnDestroyed += OnObjectDestroyed;
    }

    private void OnObjectDestroyed(bool harmful) {
        if (!harmful) {
            Score += 10;
        } else {
            LoseLife();
        }
    }

    private void LoseLife() {
        remainLives--;

        lifeCounter.LoseLife();
        Debug.Log(remainLives);
        if (remainLives <= 0) {
            scoreText.gameObject.SetActive(false);

            gameOverGroup.SetActive(true);
            Text gameOverText = gameOverGroup.GetComponentInChildren<Text>();
            gameOverText.text = string.Format(gameOverText.text, score);
        }
    }
}
