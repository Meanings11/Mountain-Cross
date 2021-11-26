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
    void OnEnable() {
        gameObject.GetComponent<Button>().interactable = true;
    }

    void Update()
    {
        priceText.text = "$" + StoreManager.GetComponent<StoreManager>().storeItems[2,itemId].ToString();
    }

    public void BtnOnClick() { gameObject.GetComponent<Button>().interactable = false; }
}
