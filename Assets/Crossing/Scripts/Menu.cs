using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Globalization;

public class Menu : MonoBehaviour
{

    public GameObject WinUI;
    public GameObject OverUI;

    public GameObject pauseMenu;
    public AudioMixer audioMixer;

    public Text cherryTextNum, WinTotalPoint, OverTotalPoint;
    public static Menu Ins;

    private void Awake()
    {
        Ins = this;
    }
    public void GameOver(bool IsWin)
    {
        Time.timeScale = 0f;

        // Get total score
        WinTotalPoint.text = "Earned " + cherryTextNum.text + " in total";
        OverTotalPoint.text = "Earned " + cherryTextNum.text + " in total";

        // Set global score
        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

        int score = Int32.Parse(cherryTextNum.text.TrimStart('$'));
        PlayerPrefs.SetInt("totalGameScore", currentGameScore + score);

        if (IsWin)
        {
            WinUI.SetActive(true);
        }
        else
        {
            // Debug.Log(IsWin);
            OverUI.SetActive(true);
        }
        
        SceneManager.LoadScene("BoardScene");
        // StartCoroutine(LoadEndScene());
    }

    // IEnumerator LoadEndScene() {
    //     yield return new WaitForSeconds(1.2f);
    //     Debug.Log("Win");
    //     SceneManager.LoadScene("BoardScene");
    // }

    // public void PlayGame() {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

}
