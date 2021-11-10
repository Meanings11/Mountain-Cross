using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEndCol : MonoBehaviour
{
    public GameObject Win;
    public bool IsEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (IsEnd)
            {
                Menu.Ins.GameOver(true);
                Destroy(this.gameObject);
                return;
            }
            // UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            // StartCoroutine(LoadEndScene());
        }
    }

    // IEnumerator LoadEndScene() {
    //     yield return new WaitForSeconds(3f);
    //     SceneManager.LoadScene("BoardScene");
    // }
}
