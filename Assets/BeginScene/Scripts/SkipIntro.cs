using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipIntro : MonoBehaviour
{
    public Button skipButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = skipButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BoardScene");
        PlayerPrefs.SetInt("showGameRules", 1);
    }
}
