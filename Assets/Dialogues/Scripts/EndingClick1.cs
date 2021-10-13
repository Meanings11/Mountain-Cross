using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingClick1 : MonoBehaviour
{
    // public Button continueBtn;

    public Text nameText;
    public Text dialogueText;
    public Image character;
    public AudioSource typeSound;

    public GameObject choice1Btn;
    public GameObject choice2Btn;

    public GameObject continueBtn;

    private Queue<Dialogue> dialogues = new Queue<Dialogue>();
    
    private int dialoguesCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Button btn = continueBtn.GetComponent<Button>();
        // btn.onClick.AddListener(TaskOnClick);

        // set choice buttons to invisible
        choice1Btn.SetActive(false);
        choice2Btn.SetActive(false);

        // dialogues
        string[] lines = {"Kidnapper:Do you want to trade yourself with\nyour bride to rescue her now, or\nkeep playing so that I can give you\nsome other options?", "Groom:I wanna..."};
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            TaskOnClick();
        }
    }

    // Update is called once per frame
    private void TaskOnClick()
    {
        if (dialogues.Count != 0) {
            typeSound.Play();
            Dialogue currentDialogue = new Dialogue();
            currentDialogue = dialogues.Dequeue();
            dialoguesCounter++;

            nameText.text = currentDialogue.name;
            character.sprite = currentDialogue.sprite;
            dialogueText.text = currentDialogue.sentence.Replace("\\n", "\n");

            if (dialoguesCounter == 2) {
                choice1Btn.SetActive(true);
                choice2Btn.SetActive(true);
                continueBtn.SetActive(false);
            }
        }
    }
}
