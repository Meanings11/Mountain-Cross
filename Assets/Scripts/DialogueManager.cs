using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Image character;
	
	public Queue<Dialogue> dialogues;

    // public Queue<string> sentences;

    public void StartDialogue (Queue<Dialogue> allDialogue) {
        // Debug.Log("Starting conversation with " + dialogue.name);
		dialogues = allDialogue;
		// Dialogue currentDialogue = dialogues.Dequeue();
		// nameText.text = currentDialogue.name;
		// character.sprite = currentDialogue.sprite;
		// dialogueText.text = currentDialogue.sentence;

			// nameText.text = dialogue.name;
			// character.sprite = dialogue.sprite;
			// sentences = new Queue<string>();
			// sentences.Clear();
			// foreach (string sentence in dialogue.sentences) {
			// 	// Debug.Log(sentence);
			// 	sentences.Enqueue(sentence);
			// }
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue() {
        if (dialogues.Count == 0) {
            EndDialogue();
            return;
        }
		Dialogue currentDialogue = dialogues.Dequeue();
		nameText.text = currentDialogue.name;
		character.sprite = currentDialogue.sprite;
		dialogueText.text = currentDialogue.sentence;
    }

    void EndDialogue() {
        Debug.Log("End of converation");
    }

}
