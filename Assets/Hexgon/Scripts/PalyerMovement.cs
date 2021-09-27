using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PalyerMovement : MonoBehaviour
{
    public Text GameoverText;
    public Text TotalPoint;
    public Text pointText;

    public GameObject Canvas;
    public GameObject CenterPlayer;

    public bool firstHit = true;

    public float moveSpeed = 500f;
    
    float movement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // set gameover to invisible
        GameoverText.gameObject.SetActive(false);
        TotalPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // movement = Input.GetAxisRaw("Horizontal");

        // touchscreen movement
        if (Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                RotateClockwise();
            } else
            {
                RotateCounterClockwise();
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            movement = 0f;
        }

        // keyboard movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateCounterClockwise();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateClockwise();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            movement = 0f;
        }

    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // StartCoroutine(LoadEndScene());
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("collide");
        // SceneManager.LoadScene("ReturnScene", LoadSceneMode.Additive);
        Canvas.gameObject.SetActive(false);
        // CenterPlayer.gameObject.SetActive(false);

        if (firstHit == true)
        {
            TotalPoint.text = "Earned $" + pointText.text.ToString() + " in total";
            GameoverText.gameObject.SetActive(true);
            TotalPoint.gameObject.SetActive(true);
        }
        
        firstHit = false;
        StartCoroutine(LoadEndScene());
        // SceneManager.LoadScene("MiniGame-1", LoadSceneMode.Single);
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MiniGame-1");
    }

    private void RotateClockwise() {
        movement = 1;
    }

    private void RotateCounterClockwise(){
        movement = -1;
    }
}
