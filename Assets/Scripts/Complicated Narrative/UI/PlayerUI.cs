using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	public RectTransform interactionUI;

	public RectTransform interactionButtonPrefab;

	//adds buttons to the interaction UI panel, based on the actions of the object
	public void addInteractions(List<NarrativeEvent> actions){

		foreach (NarrativeEvent action in actions) {

			RectTransform button = Instantiate (interactionButtonPrefab, interactionUI);

			button.GetComponent<Button> ().onClick.AddListener (action.action);

			button.GetComponentInChildren<Text> ().text = action.actionName;

			LayoutRebuilder.MarkLayoutForRebuild (interactionUI);
		}

	}

	//removes buttons from interaction UI
	public void removeInteractions(){

		print ("Destroyed");

		int childCount = interactionUI.childCount;

		for(int i = 0; i < childCount; i++){

			Destroy (interactionUI.transform.GetChild(i).gameObject);

			LayoutRebuilder.MarkLayoutForRebuild (interactionUI);
		}

	}
}
