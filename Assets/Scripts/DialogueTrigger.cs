using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Queue<Dialogue> dialogues;
	
	public void TriggerDialogue() {

		string[] lines = System.IO.File.ReadAllLines("Assets/Dialogues/Test_Dialogue.txt");
		dialogues = new Queue<Dialogue>();
		foreach (string line in lines) {
			Dialogue currentDialogue = new Dialogue();
			currentDialogue.name = line.Substring(0, line.IndexOf(':'));
			currentDialogue.sentence = line.Substring(line.IndexOf(':')+1, line.Length-line.IndexOf(':')-1);
			currentDialogue.sprite = Resources.Load<Sprite>("Sprites/" + line.Substring(0, line.IndexOf(':')));
			dialogues.Enqueue(currentDialogue);
		}

		// Dialogue dialogue1 = new Dialogue();
		// dialogue1.name = "a";
		// dialogue1.sentence = "hello";
		// dialogue1.sprite = Resources.Load<Sprite>("Sprites/groom");

		// Dialogue dialogue2 = new Dialogue();
		// dialogue2.name = "b";
		// dialogue2.sentence = "hello hello";
		// dialogue2.sprite = Resources.Load<Sprite>("Sprites/bride");
		
		// dialogues = new Queue<Dialogue>();
		// dialogues.Enqueue(dialogue1);
		// dialogues.Enqueue(dialogue2);

		// dialogue.name = "Sam";
		// dialogue.sentences = new string[] {"hello","bye","good"};
		// dialogue.sprite = Resources.Load<Sprite>("Sprites/groom");
		FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
	}

    void Start() {
        TriggerDialogue();
    }

}
