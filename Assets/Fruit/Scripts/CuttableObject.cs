using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour {

    public delegate void ObjectDestoryedHandler(bool harmful);
    public event ObjectDestoryedHandler OnDestroyed;

    public int destoryTime = 2;
    public GameObject effect;
    AudioSource audioSource;
    public AudioClip sliceAudio;
    public AudioClip hitAudio;
    public bool harmful;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Cut") {
            if (harmful) {
                audioSource.PlayOneShot(hitAudio);
            } else {
                audioSource.PlayOneShot(sliceAudio);
            }
            
            if (OnDestroyed != null) {
                OnDestroyed(harmful);
            }
            Destroy(gameObject);
            var instance = Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(instance, 1);
        }
    }


}
