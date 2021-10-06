using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // UI
    public GameObject startButton;
    public Text gameTimerText;
    public Text respawnTimerText;
    public Text scoreText;
    public Image gameOverImage;

    // Sound
    // public AudioSource flySound;

    // Parameters
    public float gameTime = 60;
    public float scrollSpeed = 1.5f;
    public bool isStart = false;
    public bool isGamePaused = false;
    public bool isGameOver = false;
    private int score = 0;
    private float leftTime;
    private float speedTimer;

    void Awake() {
        if (instance == null) {
            instance = this;
        }    
        else if (instance != this) {
            Destroy(gameObject);
        }
        leftTime = gameTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnTimerText.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        Time.timeScale = 0;

        // flySound = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        
        if (leftTime <= 0) 
        {
            GameOver();
        } else if (isStart){
            
                leftTime -= Time.unscaledDeltaTime;
                speedTimer += Time.unscaledDeltaTime;
                
                gameTimerText.text = leftTime.ToString("0");

                // speed up based on the gaming time
                if (speedTimer > 5.0) {
                    scrollSpeed +=  speedTimer * (float)0.05;
                    speedTimer = 0;
                }
        }


    }


    // Score
    public void BirdScored(int newScore) {
        if (isGamePaused) return;

        score += newScore;
        scoreText.text = "Score: " + score.ToString();
    }


    public void StartGame()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
        isStart = true;

        // flySound.Play();
    }

    public void GamePause()
    {
        // Time.timeScale = 0;
        // isGamePaused = true;

        // flySound.Stop();
        Time.timeScale = 0;
        SceneManager.LoadScene("PauseMenu");
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        gameOverImage.gameObject.SetActive(true);

        // flySound.Stop();

        // Set score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        PlayerPrefs.SetInt("totalGameScore", currentGameScore+score*10);

        // jump back to main board
        // SceneManager.LoadScene("BoardScene");
        Time.timeScale = 1;
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // API to main board
    public int getScore() {
        return score;
    }
}
