using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BeginScene : MonoBehaviour
{
    public VideoPlayer beginVideo;

    // Start is called before the first frame update
    void Start()
    {
        beginVideo = GetComponent<VideoPlayer>();
        beginVideo.Play();
        beginVideo.loopPointReached += CheckOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        StartCoroutine(LoadEndScene());
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BeginDialogue");
    }
}
