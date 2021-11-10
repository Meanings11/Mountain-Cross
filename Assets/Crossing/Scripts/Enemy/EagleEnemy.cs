using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEnemy : Enemy
{



    public Transform top, bottom;

    private float topY, bottomY;

    public float speed;

    private bool isUp;


    public Bullect Bullect;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        topY = top.position.y;
        bottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);

        StartCoroutine(OnAtt());
    }

    IEnumerator OnAtt()
    {
        while (true)
        {
            if (Vector3.Distance(PlayerController.Ins.transform.position,transform.position)<12)
            {
                Instantiate(Bullect,transform.position, Bullect.transform.rotation).OnMoveV3=(PlayerController.Ins.transform.position - transform.position).normalized;
            }
            yield return new WaitForSeconds(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }

  new  void MoveMent()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topY)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < bottomY)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isUp = true;
            }
        }



    }
}
