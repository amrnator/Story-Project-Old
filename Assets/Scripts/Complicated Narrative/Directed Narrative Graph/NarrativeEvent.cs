using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, System.Serializable]
public class NarrativeEvent : MonoBehaviour {

	public string actionName;
	[HideInInspector]
	public ConditionList globalConditionList;
	[HideInInspector]
	public List<Precondition> preconditions = new List<Precondition>();
	[HideInInspector]
	public List<Postcondition> postConditions = new List<Postcondition>();
	[HideInInspector]
	public string[] choices;
	[HideInInspector]
	public NarrativeNode narrativeNode;

//	void Awake() {
//		//get the condition list
//		globalConditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();
//
//		narrativeNode = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<NarrativeNode>();
//	}
//
//
//	public void Update()
//	{
//		#if UNITY_EDITOR
//		choices = globalConditionList.ToStringArray();
//		#endif
//	}

	public virtual void action (){
		print ("Whatever");
	}

}
