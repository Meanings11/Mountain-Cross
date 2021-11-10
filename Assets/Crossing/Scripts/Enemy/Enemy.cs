using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人脚本
public class Enemy : MonoBehaviour
{
    public Transform leftPoint, rightPoint;
    public float Speed;
    public Rigidbody2D rb;

    protected Animator anim;
    protected AudioSource deathAudio;

    public bool IsMove = false;
    public bool faceLeft = true;
    public GameObject FallObj;

    public float leftX, rightX;


    public float IsOnup=0.5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        if (leftPoint!=null)
        {
            leftX = leftPoint.position.x;
            rightX = rightPoint.position.x;
        }
        if (IsMove)
        {
            StartCoroutine(MoveMent());
        }
    }
  public  IEnumerator MoveMent()
    {
        while (true)
        {

            if (faceLeft)
            {
                anim.SetBool("Move", true);
                transform.Translate(Vector3.right * 0.02f * -Speed);

                yield return new WaitForSeconds(0.02f);
                if (transform.position.x < leftX)
                {
                      transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                    anim.SetBool("Move", false);
                    yield return new WaitForSeconds(2f);

                }
          

                
            }
            else
            {

                anim.SetBool("Move", true);

                transform.Translate(Vector3.right * 0.02f * Speed);
                yield return new WaitForSeconds(0.02f);
                if (transform.position.x > rightX)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                    anim.SetBool("Move", false);
                    yield return new WaitForSeconds(2f);
                }
            }
        }
     

    }
    public void Death()
    {
        if (FallObj!=null)
        {
            Instantiate(FallObj, transform.position, FallObj.transform.rotation);
        }
        
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        if (IsDes)
        {
            return;
        }
        IsDes = true;
        anim.SetTrigger("death");
        deathAudio.Play();
    }
    public bool IsDes;
}
