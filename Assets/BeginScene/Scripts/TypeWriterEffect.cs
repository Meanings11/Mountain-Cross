using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour
{
    private string fullText = "Once upon a time, a couple were happily getting married at their wedding. All of a sudden, a kidnapper came and took the bride. The groom then spared no effort to rescue the bride. Finally he came to the cave...";

    public Text textDisplay;

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
    }

    IEnumerator ShowText() {
        for (int i = 0; i < fullText.Length; i++) {
            textDisplay.text = fullText.Substring(0, i+1);
            yield return new WaitForSeconds(0.1f);
        }

        // load begin dialogues
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BeginDialogue");
    }
}
