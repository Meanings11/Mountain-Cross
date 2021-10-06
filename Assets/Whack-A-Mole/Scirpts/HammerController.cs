using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class HammerController : MonoBehaviour
{
    public GameObject hammer;
    public AudioSource hitAudio;
    public AudioClip timerAudio;
    public Text timer;
    public Text scoreText;
    public Text TotalPoint;
    public Text GameoverText;

    public float timeRemaining = 30f;

    private int score = 0;
    private float speed = 10;
    private bool firstHit = true;
    private Vector3 pos;
    private MoleGenerator ms;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MoleGenerator>();
        hitAudio = GetComponent<AudioSource>();

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) {
            moveHammer();
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
        } else {
            timer.text = "00:00:00";
            GameoverText.text = "Times Up!";
            if (firstHit == true) {
                hitAudio.PlayOneShot(timerAudio);
            }
            firstHit = false;

            EndGame();
        }
        // if (Input.GetButtonDown("Fire1") && ms.timeRemaining > 0) {
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //     RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //     if (hit.collider != null) {
        //         hitAudio.Play();
        //         Destroy(hit.transform.gameObject);
        //         score += 10;
        //         scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
        //         ms.Spawn();
        //     }
        // }
    }

    void moveHammer()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            touchPosition.z = hammer.transform.position.z - Camera.main.transform.position.z;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // touchPosition.y = hammer.transform.position.y;
            hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, touchPosition, Time.deltaTime * speed);
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    void OnCollisionEnter2D(Collision2D col) {
        // if (Input.GetButtonDown("Fire1")) {
            hitAudio.Play();
            hammer.gameObject.SetActive(false);

            // update score while hit the mole
            score += 10;
            scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
            DestroyImmediate(ms.mole, true);
            ms.Spawn();
        // }
    }

    void EndGame() {
        // Destroy the mole
        // Destroy(moleObject);
        ms.moleObject.gameObject.SetActive(false);

        // Get total score
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
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("BoardScene");
    }
}
