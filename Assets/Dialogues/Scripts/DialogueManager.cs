using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Image character;
	public AudioSource typeSound;
    
	public bool clicked;

	public Queue<Dialogue> dialogues;

	// Start is called before the first frame update
    void Start()
    {
        typeSound = GetComponent<AudioSource> ();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!clicked) {
                TriggerDialogue();
                clicked = true;
            } else {
                DisplayNextDialogue();
            }
        }
    }

	public void TriggerDialogue() {

		string[] lines = System.IO.File.ReadAllLines("Assets/Dialogues/Resources/Test_Dialogue.txt");
		dialogues = new Queue<Dialogue>();
		foreach (string line in lines) {
			Dialogue currentDialogue = new Dialogue();
			currentDialogue.name = line.Substring(0, line.IndexOf(':'));
			currentDialogue.sentence = line.Substring(line.IndexOf(':')+1, line.Length-line.IndexOf(':')-1);
			currentDialogue.sprite = Resources.Load<Sprite>("Sprites/" + line.Substring(0, line.IndexOf(':')));
			dialogues.Enqueue(currentDialogue);
		}

		StartDialogue(dialogues);
	}

    public void StartDialogue(Queue<Dialogue> allDialogue) {
		dialogues = allDialogue;
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue() {
		typeSound.Play();
        if (dialogues.Count == 0) {
			typeSound.Stop();
            EndDialogue();
            return;
        }
		Dialogue currentDialogue = dialogues.Dequeue();
		nameText.text = currentDialogue.name;
		character.sprite = currentDialogue.sprite;
		dialogueText.text = currentDialogue.sentence.Replace("\\n", "\n");
    }

    void EndDialogue() {
		SceneManager.LoadScene("BoardScene");
    }
}
