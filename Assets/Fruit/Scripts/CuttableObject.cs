using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour {

    public delegate void ObjectDestoryedHandler(bool harmful);
    public event ObjectDestoryedHandler OnDestroyed;

    public int destoryTime = 2;
    public GameObject effect;
    public bool harmful;


    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Cut") {
            if (harmful) {
                SceneController.instance.playBomb();
            } else {
                SceneController.instance.playSlice();
            }
            
            if (OnDestroyed != null) {
                OnDestroyed(harmful);
            }

            Vector3 temp = transform.position;
            Destroy(gameObject);
            var instance = Instantiate(effect, temp, Quaternion.identity);
            Destroy(instance, 1);
        }
    }


}
