using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenObject : MonoBehaviour
{
    public float speed;
    public AudioSource eat;

    public int type = 0; //normal mos

    private Animator _animator;
    // private bool isDead = false;
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
       if (type == 0) transform.position += ((Vector3.left * speed) * Time.deltaTime);
       else {
           transform.Translate(new Vector3(-1f, 0.5f*Mathf.Sin(Time.time),0) * speed * Time.deltaTime);
       }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        {
            if (other.GetComponent<Player>() != null) {
                mosCollider.enabled = false;
                eat.Play();
                
                if (type == 0) GameManager.instance.BirdScored (1);
                else GameManager.instance.BirdScored (3);

                _animator.SetTrigger("mosDie");
                Destroy(gameObject,1f);
                // isDead = true;

            }
        }
    }
}
