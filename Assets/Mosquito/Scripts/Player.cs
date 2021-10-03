using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 2.4f;
    public bool isDead = false;
    private Rigidbody2D rb;
    AudioSource audio;
    public AudioClip fall;
    public AudioClip jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.instance.isGameOver) rb.velocity = Vector2.up * velocity;
            audio.PlayOneShot(jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        audio.PlayOneShot(fall);
        GameManager.instance.GameOver();
    }
}
