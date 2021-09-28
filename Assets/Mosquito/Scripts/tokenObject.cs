using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenObject : MonoBehaviour
{
    public float speed;
    
    // Update is called once per frame

    
    private void Awake() {
        speed = GameManager.instance.scrollSpeed;    
    }

    void Update()
    {
        transform.position += ((Vector3.left * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        {
            if (other.GetComponent<Player>() != null) {
                GameManager.instance.BirdScored ();
                Destroy(gameObject);
            }
        }
    }
}
