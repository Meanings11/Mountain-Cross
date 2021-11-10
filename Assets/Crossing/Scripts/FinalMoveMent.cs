using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public float speed, jumpForce;
    public Transform groundCheck;
    public LayerMask ground;
    bool isJump, isGround, isJumpPress;
    int jumpCount;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isJumpPress = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(transform.position, 0.2f, ground);
        GroundMoveMent();
        Jump();
        SwitchAnim();
    }

    void GroundMoveMent()
    {
        float hMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(hMove * speed, rb.velocity.y);
        if (hMove != 0)
        {
            transform.localScale = new Vector3(hMove, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }

        if (isJumpPress && jumpCount > 0)
        {
            if (isGround)
            {
                isJump = true;
                jumpCount--;
                rb.velocity = Vector2.up * jumpForce;
                isJumpPress = false;
            }
            if (isJump)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpCount--;
                isJumpPress = false;
            }
        }
    }

    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

}
