using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 2.4f;
    public bool isDead = false;
    private Rigidbody2D rb;
    AudioSource playerAudio;
    public AudioClip fall;
    public AudioClip jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.instance.isGameOver) rb.velocity = Vector2.up * velocity;
            playerAudio.PlayOneShot(jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        playerAudio.PlayOneShot(fall);
        GameManager.instance.GameOver();
    }
}
