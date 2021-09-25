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
    public Text gameTimerText;
    public Text respawnTimerText;
    public Text scoreText;
    public Image gameOverImage;


    // Parameters
    public float gameTime = 60;
    public float respawnTime = 2;
    public float scrollSpeed = 1.5f;
    public bool isGamePaused = false;
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
        respawnTimerText.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        
        if (gameTime <= 0) 
        {
            GameOver();
            enabled = false;
        }

        if( isGamePaused)
        {
            respawnTimerText.gameObject.SetActive(true);
            respawnTime -= Time.unscaledDeltaTime;

            respawnTimerText.text = "Restarting in " + (respawnTime).ToString("0");

            if(respawnTime < 0)
            {
                RestartGame();
            }

        } else {
            gameTime -= Time.unscaledDeltaTime;
            gameTimerText.text = gameTime.ToString("0");
        }

    }


    // Score
    public void BirdScored() {
        if (isGamePaused) return;

        score++;
        scoreText.text = "Score: " + score.ToString();
    }


    public void StartGame()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void GameOver()
    {
         Time.timeScale = 0;
         isGameOver = true;
         gameOverImage.gameObject.SetActive(true);
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
