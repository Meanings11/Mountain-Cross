using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingChoice : MonoBehaviour
{
    public Button choice1;
    public Button choice2;
    public Button choice3;

    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = choice1.GetComponent<Button>();
        Button btn2 = choice2.GetComponent<Button>();
        Button btn3 = choice3.GetComponent<Button>();

		btn1.onClick.AddListener(TaskOnEndingOne);
        btn2.onClick.AddListener(TaskOnEndingTwo);
        btn3.onClick.AddListener(TaskOnEndingThree);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnEndingOne() {
        SceneManager.LoadScene("EndingDialogue1");
    }

    void TaskOnEndingTwo() {
        SceneManager.LoadScene("EndingDialogue2");
    }

    void TaskOnEndingThree() {
        SceneManager.LoadScene("EndingDialogue3");
    }
}
