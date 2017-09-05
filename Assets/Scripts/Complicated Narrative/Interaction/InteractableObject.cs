using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public float radius = 3f;

    bool isFocus = false;

	bool isClose = false;

    PlayerBehavior playerBehavior;

	List<NarrativeEvent> actions;

    Transform player;

	PlayerUI UI;

    void Start() {
        playerBehavior = FindObjectOfType<PlayerBehavior>();
		NarrativeEvent[] acts = GetComponents<NarrativeEvent>();
		UI = GameObject.FindGameObjectWithTag ("UI").GetComponent<PlayerUI>();

		actions = new List<NarrativeEvent> ();

		foreach (NarrativeEvent a in acts) {
			if (a.enabled) {
				actions.Add (a);
			}
		}
    }

    void Update() {

        if (isFocus) {
            float distance = Vector3.Distance(player.position, transform.position);
			if (distance <= radius && !isClose) {
                //interact here
				//add the available actions of this object to the menu
				UI.addInteractions(actions);

				isClose = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        playerBehavior.PlayerCommandInteract(this);
    }

    public void OnDefocused()
    {
		isFocus = false;
		isClose = false;
        player = null;
		UI.removeInteractions ();
    }

//    public void activate() {
//        action.action();
//        print("Object activated");
//    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
