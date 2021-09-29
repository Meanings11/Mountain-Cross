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

	public Queue<Dialogue> dialogues;

	// Start is called before the first frame update
    void Start()
    {
        typeSound = GetComponent<AudioSource> ();
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
