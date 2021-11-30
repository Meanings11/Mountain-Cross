using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitRules : MonoBehaviour
{
    public AudioSource closeRuleSound;
    public Button exitButton;
    public GameObject rule;

    // Start is called before the first frame update
    void Start()
    {
        closeRuleSound = GetComponent<AudioSource>();

        Button btn = exitButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        int isRuleShow = PlayerPrefs.GetInt("showGameRules", 0);
        if (isRuleShow == 1) {
            closeRuleSound.Play();
            rule.gameObject.SetActive(false);
            PlayerPrefs.SetInt("showGameRules", 0);
        }
    }

}
