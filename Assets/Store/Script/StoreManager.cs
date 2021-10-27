using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


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
        storeItems[2,1] = 100;
        storeItems[2,2] = 2000;
        
    }

    public void Buy() 
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        if (currentGameScore >= storeItems[2, ButtonRef.GetComponent<ProductButton>().itemId])
        {
            currentGameScore -= storeItems[2, ButtonRef.GetComponent<ProductButton>().itemId];
            PlayerPrefs.SetInt("totalGameScore", currentGameScore);
        } else {
            EditorUtility.DisplayDialog("Cannot afford the item!","","OK", "");
        }
    }
}
