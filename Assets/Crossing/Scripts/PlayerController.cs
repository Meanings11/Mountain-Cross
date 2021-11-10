using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Ins;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isHurt, isGround;
    private float timeRemaining = 30f;
    public Text timer;
    public UICol joystick;
    public LayerMask ground;
    public Collider2D coll, disColl;
    public Transform cellCheck, groundCheck;
    public Text cherryTextNum, HPTextNum;
    [Space]
    public float speed, jumpforce;
    [SerializeField]
    private int gemCount, jumpCount;

    private static int cherryCount;

    private static int hP = 1;

    public float IsBulkTimer;
    public float IsDownsizingTimer;

    public static float Addjumpforce;
    public bool IsInit = false;

    public static int HP { get => hP; set
        {
            if (value > 5)
            {
                return;
            }
            if (value < 0)
            {
                return;
            }
            hP = value;
            if (hP <= 0)
            {
                Menu.Ins.GameOver(false);
            }
        }
    }

    private void Awake()
    {
        Ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick = UICol.Ins;
        //  Ins = this;
        if (IsInit)
        {
            cherryCount = 0;
            Addjumpforce = 0;
            HP = 5;
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hitimer -= 0.02f;
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);

        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Update()
    {
        HPTextNum.text = hP.ToString();
        if (IsBulkTimer > 0)
        {
            transform.localScale = Vector3.one * 1.5f;
        }
        else if (IsDownsizingTimer > 0)
        {
            transform.localScale = Vector3.one * 0.6f;
        }
        else
        {
            transform.localScale = Vector3.one ;
        }
        IsDownsizingTimer -= Time.deltaTime;
        IsBulkTimer -= Time.deltaTime;
        Crouch();
        ClickButtonJump();

        // cherryTextNum.text = cherryCount.ToString();
        Debug.Log(cherryCount);
        if (cherryCount == 0 || cherryCount == 00) {
            cherryTextNum.text = "$0";
        } else {
            cherryTextNum.text = "$" + cherryCount.ToString();
        }

        HPTextNum.text = HP.ToString();

        // timer for 30s
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");
        } else {
            timer.text = "00:00:00";
            Menu.Ins.GameOver(true);
        }
    }
    
    void Movement()
    {
        float hMove = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            hMove = Input.GetAxis("Horizontal");
        }
        else if (joystick.MoveX != 0)
        {
            hMove = joystick.MoveX;
        }

        rb.velocity = new Vector2(hMove * speed * Time.fixedDeltaTime, rb.velocity.y);
        float faceDirection =hMove;
        //player movement
        if (hMove != 0)
        {
            anim.SetFloat("running", Mathf.Abs(hMove));
        }
        else
        {
            anim.SetFloat("running", 0);
        }

        if (joystick == null)
        {
            if (faceDirection != 0)
            {
                if (faceDirection > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (faceDirection < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        else
        {
            if (faceDirection > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
            else if (faceDirection < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }

    void SwitchAnim()
    {
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) <= 0.1)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
                anim.SetFloat("running", 0);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
           
        }
    }

    // gain points by eating cherry
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Collection")
        {
            SoundManager.instance.CollectionAudioPlay();
            if (collider.GetComponent<Animator>()!=null)
            {
                collider.GetComponent<Animator>().Play("collectionGot");
            }
            else
            {
                collider.GetComponent<Collection>().Death();
            }
          
        }
    }

    // destroy the enermy
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling") && transform.position.y - enemy.transform.position.y > enemy.IsOnup)
            {
                if (enemy.IsDes==false)
                {
                    enemy.JumpOn();
                    Jump();
                }
            }
            // hurt
            else
            {
                OnHit(collider.transform);
            }
        }
    }

    public void OnHit(Transform obj)
    {
        if (Hitimer > 0)
        {
            return;
        }
        Hitimer = 0.2f;
        if (transform.position.x < obj.transform.position.x)
        {
            SoundManager.instance.HurtAudioPlay();
            isHurt = true;
            rb.velocity = new Vector2(-10, rb.velocity.y); HP--;
        }
        else if (transform.position.x > obj.transform.position.x)
        {
            SoundManager.instance.HurtAudioPlay();
            isHurt = true;
            rb.velocity = new Vector2(10, rb.velocity.y); HP--;
        }
    }
    public void OnHit(Vector3 obj)
    {
        if (Hitimer > 0)
        {
            return;
        }
        Hitimer = 0.2f;
        if (transform.position.x < obj.x)
        {
            SoundManager.instance.HurtAudioPlay();
            isHurt = true;
            rb.velocity = new Vector2(-10, rb.velocity.y); HP--;
        }
        else if (transform.position.x > obj.x)
        {
            SoundManager.instance.HurtAudioPlay();
            isHurt = true;
            rb.velocity = new Vector2(10, rb.velocity.y); HP--;
        }
    }
    public float Hitimer;
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                disColl.enabled = false;
                anim.SetBool("crouching", true);
            }
            else
            {
                disColl.enabled = true;
                anim.SetBool("crouching", false);
            }
        }
    }

    void ClickButtonJump()
    {
        if (isGround)
        {
            jumpCount = 2;
        }

        if ( Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpCount--;
            Jump();
        }
      
    }
    public void JumpP()
    {
        if (jumpCount > 1)
        {
            jumpCount--;
            SoundManager.instance.JumpAudioPlay();
            rb.velocity = Vector2.up * (jumpforce + Addjumpforce);
            anim.SetBool("jumping", true);
        }
    }

    void Jump()
    {
        SoundManager.instance.JumpAudioPlay();
        rb.velocity = Vector2.up * (jumpforce + Addjumpforce);
        anim.SetBool("jumping", true);
    }

    public void CherryPlus()
    {
        // cherryCount++;
        cherryCount += 10;
    }
}
