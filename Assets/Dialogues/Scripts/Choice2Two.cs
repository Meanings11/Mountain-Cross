using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choice2Two : MonoBehaviour
{
    public Button choice2btn;

    public AudioSource changeSceneSound;

    // Start is called before the first frame update
    void Start()
    {
        choice2btn = GetComponent<Button>();
        choice2btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TaskOnClick() {
        changeSceneSound.Play();
        PlayerPrefs.SetInt("endlessMode", 2);
        // change scene to main board
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BoardScene");
    }
}
