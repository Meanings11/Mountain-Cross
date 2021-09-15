using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class returnAction : MonoBehaviour
{
    public Button returnButton;
    private int previousSceneToLoad = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        Button btn = returnButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        SceneManager.LoadScene(previousSceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
