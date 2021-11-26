using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EndGame : MonoBehaviour
{
    public Button endButton;
    public AudioSource changeSceneSound;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = endButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TaskOnClick()
    {
        changeSceneSound.Play();
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ChooseEndScene");
    }
}
