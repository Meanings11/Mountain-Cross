using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StoreManager : MonoBehaviour
{

    public int[,] storeItems = new int[4,4];

    // Start is called before the first frame update
    void Start()
    {
        // ID
        storeItems[1,1] = 1;
        storeItems[1,2] = 2;

        // Price
        storeItems[2,1] = 10;
        storeItems[2,2] = 20;
        
    }

    public void Buy() 
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        if (currentGameScore >= storeItems[2, ButtonRef.GetComponent<ProductButton>().itemId])
        {
            currentGameScore -= storeItems[2, ButtonRef.GetComponent<ProductButton>().itemId];
            PlayerPrefs.SetInt("totalGameScore", currentGameScore);
        }
    }
}
