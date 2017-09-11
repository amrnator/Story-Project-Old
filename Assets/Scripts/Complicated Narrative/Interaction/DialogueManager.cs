using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public Queue<string> sentences;

	public TextMeshProUGUI nameText;

	public TextMeshProUGUI dialogueText;

	public Animator animator;

	void Start () {
		sentences = new Queue<string> ();
	}

	public void StartDialogue(Dialogue dialog)
	{
		animator.SetBool ("isOpen", true);

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
			return;
		}

		string sentence = sentences.Dequeue ();

		StopAllCoroutines ();

		StartCoroutine (typeSentence(sentence));
	}

	IEnumerator typeSentence(string sentence){
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;

			//wait two frames
			yield return null;
			yield return null;
		}
	}


	void EndDialogue(){
		animator.SetBool ("isOpen", false);

		print ("Convo over");
	}
}
