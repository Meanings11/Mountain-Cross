using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEndCol : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // end game if hit the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Vibrate if hit the ending sign
            #if UNITY_IPHONE || UNITY_ANDROID
            Handheld.Vibrate();
            #endif
            
            PlayerController.Ins.GameOver();
            Destroy(this.gameObject);
            return;
        }
    }
}
