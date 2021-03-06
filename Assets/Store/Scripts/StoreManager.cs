using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour
{
    public Text warningText;
    public const int ID = 1;
    public const int PRICE = 2;

    public int[,] storeItems = new int[4,4];

    // Audio 
    public AudioSource audioSource;
    public AudioClip successSound; 
    public AudioClip failSound; 

    // Start is called before the first frame update
    void Start()
    {
        // warningText = GameObject.Find("WarningText").GetComponent<Text>();
        warningText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        // ID
        storeItems[ID,1] = 1;
        storeItems[ID,2] = 2;
        storeItems[ID,3] = 3;

        // Price
        storeItems[PRICE,1] = 200;
        storeItems[PRICE,2] = 300;
        storeItems[PRICE,3] = 2000;

        // PlayerPrefs.SetInt("totalGameScore", 2000);
    }

    private void OnEnable() {
        if (warningText) warningText.gameObject.SetActive(false);
    }

    public void Buy() 
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemId = ButtonRef.GetComponent<ProductButton>().itemId;

        int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);
        if (currentGameScore >= storeItems[PRICE, itemId]) {
            // play sound
            audioSource.PlayOneShot(successSound);

            if (itemId == 1) {
                warningText.text = "Congrats! You bought the insurance and will save you from losing money";
            } else if (itemId == 2) {
                warningText.text = "Congrats! You bought the fly ticket and can choose to jump 1 - 6 steps";
            }

            // deduct money
            currentGameScore -= storeItems[PRICE, itemId];
            PlayerPrefs.SetInt("totalGameScore", currentGameScore);

            PlayerStats.addItem(itemId);
        } else {
            audioSource.PlayOneShot(failSound);
        }

        StartCoroutine(toggleWarning());
    }

    IEnumerator toggleWarning() {
        Debug.Log("toggle");
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }
}
