using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("Gameplay")]
    public int remainLives;
    public float timeRemaining = 30f;
    public ObjectSpawner[] fruitSpawners;
    public ObjectSpawner[] bombSpawners;

    [Header("Interfaces")]
    public LifeCounter lifeCounter;
    public Text GameoverText;
    public Text TotalPoint;
    public Text scoreText;
    public Text timer;

    AudioSource audioSource;

    public AudioClip timesUp;
    public AudioClip sliceAudio;
    public AudioClip hitAudio;

    private bool isGameOver = false;
    private int score = 0;
    public GameObject hitInfoUI;
    public Text hitTitle;
    public Text hitCountText;
    Dictionary<GameObject, List<bool>> swordHitObj;
    Stack<GameObject> clearObjs;
    float missTimeCounter = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);

        lifeCounter.SetLives(remainLives);

        for (int i = 0, length = fruitSpawners.Length; i < length; i++)
            fruitSpawners[i].OnObjectSpawned += OnObjectSpawned;
        for (int i = 0, length = bombSpawners.Length; i < length; i++)
            bombSpawners[i].OnObjectSpawned += OnObjectSpawned;

        // create saver
        swordHitObj = new Dictionary<GameObject, List<bool>>();
        clearObjs = new Stack<GameObject>();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
        }
        else
        {
            timer.text = "00:00:00";
            GameoverText.text = "Times Up!";
            audioSource.PlayOneShot(timesUp);

            if (!isGameOver)
            {
                EndGame();
            }
        }

        if (hitInfoUI.activeSelf)
        {
            missTimeCounter += Time.deltaTime;
            if (missTimeCounter >= 1)
            {
                missTimeCounter = 0;

                hitCountText.text = "0";
                hitInfoUI.SetActive(false);
            }
        }
    }

    void OnObjectSpawned(CuttableObject obj)
    {
        obj.OnDestroyed += OnObjectDestroyed;
    }

    void OnObjectDestroyed(bool harmful)
    {
        if (!harmful)
        {
            score += 10;

            if (score == 0 || score == 00)
            {
                scoreText.text = "$0";
            }
            else
            {
                scoreText.text = "$" + string.Format("{0:0,0}", Int32.Parse(score.ToString()));
            }
        }
        else
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        remainLives--;

        if (remainLives > 0)
        {
            lifeCounter.LoseLife();
        }
        else
        {
            EndGame();
        }
    }

    public void HitCuttable(GameObject sword, bool bomber)
    {
        // clear
        foreach (var item in swordHitObj)
        {
            if (item.Key == null)
                clearObjs.Push(item.Key);
        }
        for (int i = 0, length = clearObjs.Count; i < length; i++)
            swordHitObj.Remove(clearObjs.Pop());

        if (!swordHitObj.ContainsKey(sword))
            swordHitObj.Add(sword, new List<bool>());

        swordHitObj[sword].Add(bomber);

        bool showHitInfo = !bomber;
        int fruitCount = 0;
        for (int i = 0, length = swordHitObj[sword].Count; i < length; i++)
        {
            showHitInfo &= !swordHitObj[sword][i];
            fruitCount++;

            if (!showHitInfo)
                break;
        }

        if (showHitInfo)
        {
            // if (fruitCount > 1 && fruitCount > int.Parse(hitCountText.text))
            if (fruitCount > 1)
            {
                hitTitle.text = fruitCount.ToString() + " Fruit\nCombo";
                hitCountText.text = "+ " + fruitCount.ToString();
                hitInfoUI.SetActive(true);
            }
        }
        else
        {
            hitTitle.text = "Fruit";
            hitCountText.text = "0";
            hitInfoUI.SetActive(false);
        }
    }

    public void playBomb()
    {
        missTimeCounter = 0;

        audioSource.PlayOneShot(hitAudio);
    }

    public void playSlice()
    {
        missTimeCounter = 0;

        audioSource.PlayOneShot(sliceAudio);
    }

    void EndGame()
    {
        isGameOver = true;
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

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");
    }
}
