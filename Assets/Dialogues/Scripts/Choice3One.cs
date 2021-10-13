using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choice3One : MonoBehaviour
{
    public Button choice1btn;

    public AudioSource changeSceneSound;

    // Start is called before the first frame update
    void Start()
    {
        choice1btn = GetComponent<Button>();
        choice1btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TaskOnClick() {
        changeSceneSound.Play();
        // change scene to main board
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene3");
    }
}
