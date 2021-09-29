using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Queue<Dialogue> dialogues;
	
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

		FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
	}

    void Start() {
        TriggerDialogue();
    }

}
