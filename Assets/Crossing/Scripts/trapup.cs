using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapup : MonoBehaviour
{
    public Transform top, bottom;

    private float topY, bottomY;
    public Rigidbody2D rb;
    public float speed;
    public bool face = true;
    public bool IsMove = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (top != null)
        {
            topY = top.position.y;
            bottomY = bottom.position.y;
        }
        if (IsMove)
        {
            StartCoroutine(MoveMent());
        }
        rb = GetComponent<Rigidbody2D>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public IEnumerator MoveMent()
    {
        while (true)
        {
            if (face)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                if (transform.position.y > topY)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    face = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
                if (transform.position.y < bottomY)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    face = true;
                }
            }


        }
    }
}
