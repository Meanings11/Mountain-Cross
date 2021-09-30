using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using System;

public class Hexagon : MonoBehaviour
{
    public Rigidbody2D rb;
    private float shrinkSpeed = 1f;

    // public Text GameoverText;
    // public Text TotalPoint;
    // public Text pointText;

    // public GameObject Canvas;
    // public GameObject CenterPlayer;

    // public AudioSource hitSource;

    // public bool firstHit = true;

    // Start is called before the first frame update
    void Start()
    {
        // hitSource = GetComponent<AudioSource> ();

        rb.rotation = UnityEngine.Random.Range(0f, 360f);
        transform.localScale = Vector3.one * 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //     Debug.Log("collide");
    //     // SceneManager.LoadScene("ReturnScene", LoadSceneMode.Additive);
    //     Canvas.gameObject.SetActive(false);
    //     // CenterPlayer.gameObject.SetActive(false);
    //     hitSource.Play();

    //     if (firstHit == true)
    //     {
    //         TotalPoint.text = "Earned $" + pointText.text.ToString() + " in total";
    //         GameoverText.gameObject.SetActive(true);
    //         TotalPoint.gameObject.SetActive(true);

    //         // Set score
    //         int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
    //         int addedScore = Int16.Parse(pointText.text);
    //         PlayerPrefs.SetInt("totalGameScore", currentGameScore+addedScore);
    //     }
        
    //     firstHit = false;
        
    //     // End scene
    //     StartCoroutine(LoadEndScene());
    //     // SceneManager.LoadScene("BoardScene", LoadSceneMode.Single);
    // }

    // IEnumerator LoadEndScene() {
    //     yield return new WaitForSeconds(3.5f);
    //     SceneManager.LoadScene("BoardScene");
    // }
}
