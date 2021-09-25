using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // UI
    public GameObject startButton;

    public Text gameOverCountdown;
    public Text scoreText;


    // Parameters
    public float countTimer = 2;
    public float scrollSpeed = 1.5f;
    public bool isGameOver = false;
    private int score = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        }    
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverCountdown.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if( isGameOver )
        {
            gameOverCountdown.gameObject.SetActive(true);
            countTimer -= Time.unscaledDeltaTime;
        }

        gameOverCountdown.text = "Restarting in " + (countTimer).ToString("0");

        if(countTimer < 0)
        {
            RestartGame();
        }
    }


    // Score
    public void BirdScored() {
        if (isGameOver) return;

        score++;
        scoreText.text = "Score: " + score.ToString();
    }


    public void StartGame()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
    }


    public void RestartGame()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    // API to main board
    public int getScore() {
        return score;
    }
}
