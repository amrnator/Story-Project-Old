using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NarrativeAction : NarrativeEvent {

//    public string actionName;
//    
//    public ConditionList globalConditionList;
//
//    public List<Precondition> preconditions = new List<Precondition>();
//
//    public List<Postcondition> postConditions = new List<Postcondition>();
//
//    public string[] choices;
//
//    NarrativeNode narrativeNode;
//
//    //delegate funtions for event handling
//    public delegate void narrativeUpdate();
//    public static event narrativeUpdate actionMade;
//

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

	//delegate funtions for event handling
    public delegate void narrativeUpdate();
    public static event narrativeUpdate actionMade;

    //check conditions and execute if they are satisfied
	public override void action() {

        //check preconditions
        for (int i = 0; i < preconditions.Count; i++) {

            bool check = preconditions[i].checkCondition();

            if (!check) {
                Debug.Log("Action failed, precondition: " + preconditions[i].refrencedCondition.conditionName + " not satisfied");
                return ;
            }
        }

        //preconditions satisfied, execute post conditions
        print("Conditions satisfied executing: " + this.actionName);
        for (int i = 0; i < postConditions.Count; i++) {
            postConditions[i].execute();
        }

        //ping the Narrative Node
        narrativeNode.checkState();

        //signal to everyone else that an action has taken place
		if (null != actionMade)
        {
            actionMade();
        }

        return ;
    }
}

[System.Serializable]
public class Precondition {
    //refernce to conditioon in list
    public ConditionList.Condition refrencedCondition;

    //specified value
    public bool value;

    public ConditionList conditionList;

    public Precondition(ref ConditionList.Condition condition)
    {
        refrencedCondition = condition;
    }

    void Start() {
        //TODO maybe improve this so conditonList can be passed by constructor without being reset in Play mode. 
        //conditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();
    }

    //check if precondition is satisfied
    public bool checkCondition() {
        conditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();

        Predicate<ConditionList.Condition> condFinder = (ConditionList.Condition c) => { return c.conditionName == refrencedCondition.conditionName; };

        ConditionList.Condition trueCondition = conditionList.conditionList.Find(condFinder);

        if (trueCondition.boolean == value) {
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class Postcondition  {

    public ConditionList.Condition refrencedCondition;

    ConditionList.Condition trueCondition;

    public bool value;

    public ConditionList conditionList;

    public Postcondition(ref ConditionList.Condition condition, ConditionList glovalConditionList)
    {
        refrencedCondition = condition;

        conditionList = glovalConditionList;
    }

    void Start()
    {
        //TODO maybe improve this so conditonList can be passed by constructor without being reset in Play mode. 
        //conditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();
    }

    //change refenced condition to value
    public void execute() {
        //TODO maybe improve this so conditonList can be passed by constructor without being reset in Play mode. 
        conditionList = GameObject.FindGameObjectWithTag("NarrativeController").GetComponent<ConditionList>();

        Predicate<ConditionList.Condition> condFinder = (ConditionList.Condition c) => { return c.conditionName == refrencedCondition.conditionName; };

        trueCondition = conditionList.conditionList.Find(condFinder);

        trueCondition.boolean = value;
    }
}

