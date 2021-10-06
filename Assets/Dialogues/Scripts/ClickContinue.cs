using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickContinue : MonoBehaviour
{
    public Button continueBtn;

    public Text nameText;
    public Text dialogueText;
    public Image character;
    public AudioSource typeSound;
	private Queue<Dialogue> dialogues = new Queue<Dialogue>();

    // Start is called before the first frame update
    void Start()
    {
        Button btn = continueBtn.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        // read dialogues
        string[] lines = System.IO.File.ReadAllLines("Assets/Dialogues/Resources/Test_Dialogue.txt");
        foreach (string line in lines)
        {
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
    private void TaskOnClick()
    {
        // DialogueManager.TriggerDialogue();
        typeSound.Play();
        if (dialogues.Count == 0) {
            typeSound.Stop();
            SceneManager.LoadScene("BoardScene");
            return;
        }
        Debug.Log(nameText);
        Dialogue currentDialogue = new Dialogue();
        currentDialogue = dialogues.Dequeue();
        
        nameText.text = currentDialogue.name;
        character.sprite = currentDialogue.sprite;
        dialogueText.text = currentDialogue.sentence.Replace("\\n", "\n");
    }
}
