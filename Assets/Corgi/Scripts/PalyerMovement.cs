using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class PalyerMovement : MonoBehaviour
{
    public Text GameoverText;
    public Text TotalPoint;
    public Text pointText;

    public GameObject Canvas;
    // public GameObject CenterPlayer;
    public GameObject bonePrefab;

    public AudioSource spinSource;
    public AudioClip eatSound;
    public AudioClip hitPlayer;
    public AudioClip timesUp;

    private bool firstHit = true;

    private float moveSpeed = 500f;
    
    private float movement = 0f;
    private float timeRemaining = 30f;

    private SpriteRenderer playerRenderer;

    private GameObject spawnerObj;
    private Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spinSource = GetComponent<AudioSource> ();
        playerRenderer = GetComponent<SpriteRenderer> ();

        spawnerObj = GameObject.Find("Spawner");
        spawner = spawnerObj.GetComponent<Spawner>();

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // touchscreen movement
        if (Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                RotateClockwise();
                playerRenderer.flipX = false;
                // transform.localRotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                RotateCounterClockwise();
                playerRenderer.flipX = true;
                // transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }

        // keyboard movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateCounterClockwise();
            playerRenderer.flipX = true;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateClockwise();
            playerRenderer.flipX = false;
            // transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // stop movement
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            movement = 0f;
        }

        // end the game after 30s
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
        } else {
            GameoverText.text = "Times Up!";
            if (firstHit == true) {
                spinSource.PlayOneShot(timesUp);
            }
            firstHit = false;

            EndGame();
        }
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject healthBar = GameObject.Find("HealthBar");
        if (healthBar != null) {
            HealthBar healthBarScript = healthBar.GetComponent<HealthBar>();
            if (other.gameObject.tag == "lightCircle") {
                spinSource.PlayOneShot(hitPlayer);
                if (healthBarScript.currentHealth <= 1) {
                    if (firstHit == true) {
                        EndGame();
                    }
                    firstHit = false;
                } else {
                    healthBarScript.currentHealth--;
                }
            } else {
                if (healthBarScript.currentHealth < healthBarScript.maxHealth) {
                    healthBarScript.currentHealth++;
                    spinSource.PlayOneShot(eatSound);
                }
            }
        }
    }

    void EndGame() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("collide");
        // SceneManager.LoadScene("ReturnScene", LoadSceneMode.Additive);
        Canvas.gameObject.SetActive(false);
        bonePrefab.gameObject.SetActive(false);
        // CenterPlayer.gameObject.SetActive(false);

        // Get total score
        TotalPoint.text = "Earned " + pointText.text + " in total";
        GameoverText.gameObject.SetActive(true);
        TotalPoint.gameObject.SetActive(true);

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

        PlayerPrefs.SetInt("totalGameScore", currentGameScore + spawner.playerPoint);
        
        // End scene
        StartCoroutine(LoadEndScene());
        // SceneManager.LoadScene("BoardScene", LoadSceneMode.Single);
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("BoardScene");
    }

    private void RotateClockwise() {
        movement = 1;
    }

    private void RotateCounterClockwise(){
        movement = -1;
    }
}
