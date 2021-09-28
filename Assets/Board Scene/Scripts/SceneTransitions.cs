using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnimation;

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)) {
        //     StartCoroutine(LoadScene());
        // }
    }

    public void loadScene(int sceneIndex) {
        StartCoroutine(LoadScene(scene: sceneIndex));
    }

    public void loadScene(string sceneName) {
        StartCoroutine(LoadScene(scene: sceneName));
    }

    IEnumerator LoadScene(string scene) {
        transitionAnimation.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator LoadScene(int scene) {
        transitionAnimation.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
}
