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

    //Audio
    AudioSource audioSource;
    public AudioClip hitAudio;
    public AudioClip timerAudio;
    public AudioClip misHitAudio;

    //UI
    public Text timer;
    public Text scoreText;
    public Text TotalPoint;
    public Text GameoverText;

    public GameObject bridePanel;

    public Animator animator;

    public float timeRemaining = 30f;

    private int score = 0;
    private float speed = 10;
    private bool firstHit = true;
    private Vector3 pos;

    private GameObject moleGenerator;
    private MoleGenerator moleGt;

    // Start is called before the first frame update
    void Start()
    {
        moleGenerator = GameObject.Find("MoleGenerator");
        moleGt = moleGenerator.GetComponent<MoleGenerator>();

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // hide bride panel by default
        bridePanel.SetActive(false);

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
            moveHammer();
        } else {
            timer.text = "00:00:00";
            GameoverText.text = "Times Up!";
            if (firstHit == true) {
                audioSource.PlayOneShot(timerAudio);
                EndGame();
            }
            firstHit = false;
        }
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

        if ((Input.touchCount > 0) || Input.GetMouseButtonDown(0)) {
            Vector2 currPos2D = new Vector2(pos.x, pos.y);
            RaycastHit2D hit = Physics2D.Raycast(currPos2D, Vector2.zero);
            if (hit.collider != null) {
                // hide the old alert while hit new mole
                bridePanel.SetActive(false);

                MoleHitted();
                animator.SetTrigger("Hammer");
                
                // update score while hit the mole
                if (moleGt.isBride) {
                    score = 0;

                    // Vibrate if hit the bride
                    #if UNITY_IPHONE || UNITY_ANDROID
                    Handheld.Vibrate();
                    #endif

                    // play sound while hit the mole
                    audioSource.PlayOneShot(misHitAudio);

                    bridePanel.SetActive(true); // show lose point alert
                } else {
                    score += 10;
                    // play sound while hit the mole
                    audioSource.PlayOneShot(hitAudio);
                }

                if (score == 0 || score == 00) {
                    scoreText.text = "$0";
                } else {
                    scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
                }

                animator.SetTrigger("Unhammer");

                // disable collider
                hit.collider.enabled = false;
            }
        }
    }

    void EndGame() {
        // Destroy the mole
        // moleGt.mole.gameObject.SetActive(false);
        Destroy(moleGt.mole);
        moleGt.emptyMoleList[moleGt.hiddenIndex].gameObject.SetActive(true);

        animator.SetTrigger("Unhammer");

        // Get total score
        TotalPoint.text = "Earned " + scoreText.text + " in total";
        GameoverText.gameObject.SetActive(true);
        TotalPoint.gameObject.SetActive(true);

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

        // CultureInfo provider = new CultureInfo("en-US");
        // NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;

        // decimal pointNumber = Decimal.Parse(scoreText.text, style, provider);
        // int addedScore = Decimal.ToInt32(pointNumber);
        // Debug.Log(score);
        PlayerPrefs.SetInt("totalGameScore", currentGameScore + score);
        
        // End scene
        StartCoroutine(LoadEndScene());

        // hammer.gameObject.SetActive(false);
    }

    IEnumerator MoleHitted() {
        yield return new WaitForSeconds(0.01f);
        moleGt.mole.gameObject.SetActive(false);
        moleGt.emptyMoleList[moleGt.hiddenIndex].gameObject.SetActive(true);
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("BoardScene");
    }
}
