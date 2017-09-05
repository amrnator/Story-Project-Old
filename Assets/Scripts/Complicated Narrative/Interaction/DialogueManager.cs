using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Queue<string> sentences;

	public Text nameText;

	public Text dialogueText;

	void Start () {
		sentences = new Queue<string> ();
	}

	public void StartDialogue(Dialogue dialog)
	{
		print ("Starting Convo with: " + dialog.name);

		nameText.text = dialog.name;

		sentences.Clear();

		for(int i = 0; i < dialog.sentences.Length; i++) {
			sentences.Enqueue (dialog.sentences[i]);
		}

		displayNextSentence();
	}

	public void displayNextSentence(){

		if (sentences.Count == 0) {
			EndDialogue();	
		}

		string sentence = sentences.Dequeue ();

		dialogueText.text = sentence;
	}

	void EndDialogue(){
		print ("Convo over");
	}
}
