using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAction : NarrativeEvent {

	public Dialogue dialogue;

	void Awake() {
		//get the condition list
		globalConditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();

		narrativeNode = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<NarrativeNode>();
	}

		
	public void Update()
	{
		#if UNITY_EDITOR
		choices = globalConditionList.ToStringArray();
		#endif
	}

	public override void action(){
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}
}
