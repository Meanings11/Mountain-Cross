using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 陷阱
/// </summary>
public class Trap : MonoBehaviour
{
    public Transform leftPoint, rightPoint;

    public bool IsMove = false;
    public bool faceLeft = true;

    public Rigidbody2D rb;
    public float Speed;

    public float leftX, rightX;

    public bool IsHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
        if (leftPoint != null)
        {
            leftX = leftPoint.position.x;
            rightX = rightPoint.position.x;
        }
        if (IsMove)
        {
            StartCoroutine(MoveMent());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsHit && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().OnHit(collision.contacts[0].point);
        }
    }

    public IEnumerator MoveMent()
    {
        while (true)
        {
            if (faceLeft)
            {
                transform.Translate(Vector3.right*0.02f*-Speed);

                yield return new WaitForSeconds(0.02f);
                if (transform.position.x < leftX)
                {
                  //  transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                
                    yield return new WaitForSeconds(2f);

                }
                // Debug.Log(rb.velocity);
            }
            else
            {
                transform.Translate(Vector3.right * 0.02f * Speed);
                yield return new WaitForSeconds(0.02f);
                if (transform.position.x > rightX)
                {
                 //   transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                
                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }
}
