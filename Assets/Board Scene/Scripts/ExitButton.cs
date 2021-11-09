using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exitButton;

    public GameObject shop;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = exitButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        shop.gameObject.SetActive(false);
        GameControl.isInStore = false;
        ItemControl.instance.refreshCurrentItems();
    }
}
