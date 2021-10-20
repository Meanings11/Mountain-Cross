using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour {

    public delegate void ObjectDestoryedHandler(bool harmful);
    public event ObjectDestoryedHandler OnDestroyed;

    public bool harmful;

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Cut") {
            if (OnDestroyed != null) {
                OnDestroyed(harmful);
            }

            Destroy(gameObject);
        }
    }
}
