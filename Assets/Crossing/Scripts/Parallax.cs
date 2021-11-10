using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//视差效果
public class Parallax : MonoBehaviour
{

    public Transform cam;
    public float moveRate;
    public bool lockY;

    private float startPosX, startPosY;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPosX + moveRate * cam.transform.position.x, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPosX + moveRate * cam.transform.position.x, startPosY + transform.position.y * moveRate);
        }


    }
}
