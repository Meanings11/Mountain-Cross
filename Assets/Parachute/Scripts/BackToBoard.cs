using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadEndScene());
        // Invoke("LoadEndScene", 2f);
    }

    // void LoadEndScene()
    // {
    //     SceneManager.LoadScene("BoardScene");
    // }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");
    }
}
