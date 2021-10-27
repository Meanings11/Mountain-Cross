using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    public int itemId;
    public Text priceText;
    public GameObject StoreManager;

    // Start is called before the first frame update
    void Update()
    {
        priceText.text = "$" + StoreManager.GetComponent<StoreManager>().storeItems[2,itemId].ToString();
    }

}
