using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exitButton;

    public GameObject shop;

    public GameObject testGames;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = exitButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // show test buttons
        testGames.SetActive(true);

        shop.gameObject.SetActive(false);
        GameControl.isInStore = false;
        ItemControl.instance.refreshCurrentItems();
    }
}
