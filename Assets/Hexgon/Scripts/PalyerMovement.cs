using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PalyerMovement : MonoBehaviour
{
    public Text GameoverText;
    public Text TotalPoint;
    public Text pointText;

    public GameObject Canvas;
    public GameObject CenterPlayer;

    public AudioSource spinSource;
    public AudioClip hitPlayer;

    public bool firstHit = true;

    public float moveSpeed = 500f;
    
    float movement = 0f;

    private SpriteRenderer playerRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spinSource = GetComponent<AudioSource> ();
        playerRenderer = GetComponent<SpriteRenderer> ();

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
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("collide");
        // SceneManager.LoadScene("ReturnScene", LoadSceneMode.Additive);
        Canvas.gameObject.SetActive(false);
        // CenterPlayer.gameObject.SetActive(false);
        spinSource.PlayOneShot(hitPlayer);

        if (firstHit == true)
        {
            TotalPoint.text = "Earned $" + pointText.text.ToString() + " in total";
            GameoverText.gameObject.SetActive(true);
            TotalPoint.gameObject.SetActive(true);

            // Set score
            int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
            int addedScore = Int16.Parse(pointText.text);
            PlayerPrefs.SetInt("totalGameScore", currentGameScore+addedScore);
        }
        
        firstHit = false;
        
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
