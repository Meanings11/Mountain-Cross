using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterHouse : MonoBehaviour
{
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            // SceneManager.LoadScene(nextLevel);
            Debug.Log("nextLevel: " + nextLevel);
            SceneManager.LoadScene(nextLevel);
        }

    }
}
