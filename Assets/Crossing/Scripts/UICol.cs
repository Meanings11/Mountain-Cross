using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICol : MonoBehaviour
{
    public float MoveX;
    public float OnTimer;
    public static UICol Ins;
    // Start is called before the first frame update
    private void Awake()
    {
        Ins = this;
    }

    void Start()
    {
      
    }
    public void Jup()
    {
        PlayerController.Ins.JumpP();
    }
    // Update is called once per frame
    void Update()
    {
        //OnTimer -= Time.deltaTime;
        //if (OnTimer<=0)
        //{
        //    MoveX = 0;
        //}
    }

    public void OnMove(float x)
    {
        MoveX = x;
        OnTimer = 0.3f;
    }
}
