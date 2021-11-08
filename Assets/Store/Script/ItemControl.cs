using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Button planeButton;
    public Button stepNumButton;

    void Start()
    {

        planeButton = GameObject.Find("PlaneButton").GetComponent<Button>();
        stepNumButton = GameObject.Find("StepNumButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        int ticketNum = PlayerStats.getItemNum(2); // get plane ticket count

        if (ticketNum > 0) planeButton.gameObject.SetActive(true);
        else planeButton.gameObject.SetActive(false);
        
    }
}
