using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;



public class StoreManager : MonoBehaviour
{
    public const int ID = 1;
    public const int PRICE = 2;

    public int[,] storeItems = new int[4,4];

    // Start is called before the first frame update
    void Start()
    {
        // ID
        storeItems[ID,1] = 1;
        storeItems[ID,2] = 2;
        storeItems[ID,3] = 3;

        // Price
        storeItems[PRICE,1] = 100;
        storeItems[PRICE,2] = 150;
        storeItems[PRICE,3] = 2000;
        
    }

    public void Buy() 
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemId = ButtonRef.GetComponent<ProductButton>().itemId;

        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        if (currentGameScore >= storeItems[PRICE, itemId])
        {
            currentGameScore -= storeItems[PRICE, itemId];
            PlayerPrefs.SetInt("totalGameScore", currentGameScore);

            PlayerStats.addItem(itemId);
        } else {
            EditorUtility.DisplayDialog("Cannot afford the item!","","OK", "");
        }
    }
}
