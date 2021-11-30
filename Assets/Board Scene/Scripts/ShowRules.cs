using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRules : MonoBehaviour
{
    public AudioSource showRuleSound;
    public Button ruleButton;
    public GameObject rule;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = ruleButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        showRuleSound.Play();
        rule.gameObject.SetActive(true);
        PlayerPrefs.SetInt("showGameRules", 1);
    }
}
