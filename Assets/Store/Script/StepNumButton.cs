using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepNumButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text numText;
    public int num;
    public static bool isMovingRight = false;
    public static bool isMovingLeft = false;
    void Start()
    {
        num = 1;
        numText = GameObject.Find("StepNumText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // numText.text = num;
        
    }

    public void onClick() {
        num++;

        if (num > 6) num = 1;
        numText.text = num.ToString();
    }
}
