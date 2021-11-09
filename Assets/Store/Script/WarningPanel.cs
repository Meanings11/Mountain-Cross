using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{

    public Text warningText;
    public string text;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() {
        warningText.text = text;
    }

    public void setText(string s) {
        text = s;
    }

    public void show() {
        gameObject.SetActive(true);
    }
}
