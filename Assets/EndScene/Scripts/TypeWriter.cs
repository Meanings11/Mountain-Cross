using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TypeWriter : MonoBehaviour
{
    public string fullText;

    public Text textDisplay;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (Input.GetKeyDown(KeyCode.Escape) || !audioSource.isPlaying) {
            StartCoroutine(QuitGame());
        }
    }

    IEnumerator ShowText() {
        for (int i = 0; i < fullText.Length; i++) {
            textDisplay.text = fullText.Substring(0, i+1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator QuitGame() {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
