using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenObject : MonoBehaviour
{
    public float speed;
    public GameObject splash;
    public AudioSource eat;


    private Animator _animator;
    private bool isDead = false;
    private BoxCollider2D mosCollider;
    
    // Update is called once per frame

    
    private void Awake() {
        speed = GameManager.instance.scrollSpeed;
        _animator = GetComponent<Animator>();
        mosCollider = GetComponent<BoxCollider2D>();
        eat = GetComponent<AudioSource>();
    }

    void Update()
    {
    //    if (!isDead) 
       transform.position += ((Vector3.left * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        {
            if (other.GetComponent<Player>() != null) {
                mosCollider.enabled = false;
                eat.Play();
                GameManager.instance.BirdScored ();

                _animator.SetTrigger("mosDie");
                Destroy(gameObject,0.8f);
                isDead = true;

            }
        }
    }
}
