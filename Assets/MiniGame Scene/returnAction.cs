using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class returnAction : MonoBehaviour
{
    public Button returnButton;
    private int previousSceneToLoad = 0;
    private static GameObject sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");

        previousSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        Button btn = returnButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        sceneManager.GetComponent<SceneTransitions>().loadScene(sceneIndex: previousSceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
