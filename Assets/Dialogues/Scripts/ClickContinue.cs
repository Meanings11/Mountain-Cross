// using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickContinue : MonoBehaviour
{
    // public Button continueBtn;

    public Text nameText;
    public Text dialogueText;
    public Image character;
    public AudioSource typeSound;
    public AudioClip changeScene;

    private Queue<Dialogue> dialogues = new Queue<Dialogue>();

    private bool firstChangeClick = true;

    // Start is called before the first frame update
    void Start() {
        // Button btn = continueBtn.GetComponent<Button>();
		// btn.onClick.AddListener(TaskOnClick);

        // dialogues
        string[] lines = {"Groom:I can give you whatever you want.\nPlease let her go!",
            "Bride:Help T~~~T", "Kidnapper:Play the board game and earn $5,000,\nthen I'll consider~"};
        /* better to use json */

        foreach (string line in lines) {
            Dialogue currentDialogue = new Dialogue();
            currentDialogue.name = line.Substring(0, line.IndexOf(':'));
            currentDialogue.sentence = line.Substring(line.IndexOf(':') + 1, line.Length - line.IndexOf(':') - 1);
            currentDialogue.sprite = Resources.Load<Sprite>("Sprites/" + line.Substring(0, line.IndexOf(':')));
            dialogues.Enqueue(currentDialogue);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            TaskOnClick();
        }
    }

    // Update is called once per frame
    private void TaskOnClick() {
        if (dialogues.Count == 0) {
            if (firstChangeClick) {
                typeSound.PlayOneShot(changeScene);
            }
            firstChangeClick = false;

            // change scene to main board
            StartCoroutine(LoadEndScene());
        } else {
            typeSound.Play();
            Dialogue currentDialogue = new Dialogue();
            currentDialogue = dialogues.Dequeue();
            
            nameText.text = currentDialogue.name;
            character.sprite = currentDialogue.sprite;
            dialogueText.text = currentDialogue.sentence.Replace("\\n", "\n");
        }
    }

    IEnumerator LoadEndScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BoardScene");
        PlayerPrefs.SetInt("showGameRules", 1);
    }
}
