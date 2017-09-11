using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
	
    public delegate void OnFocusChanged(InteractableObject newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public InteractableObject focus;  // Our current focus: Item, Enemy etc.

    public LayerMask interactionMask;   // Everything we can interact with

    PlayerActions playerActions;      // Reference to our motor
    Camera cam;             // Reference to our camera

	PlayerBehavior playerBehavior;

	bool isExploring = false;

    // Get references
    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
		playerBehavior = GetComponent<PlayerBehavior> ();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit an interaction mask
            if (Physics.Raycast(ray, out hit, 100f, interactionMask))
            {
				//set focus of object
                SetFocus(hit.collider.GetComponent<InteractableObject>());
            }
        }

    }

    // Set our focus to a new focus
    void SetFocus(InteractableObject newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
        }

        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(transform);
        }

    }

	//toggle explore
	void OnMouseDown()
	{
		if (isExploring) {
			playerBehavior.PlayerCommandStop ();
			isExploring = false;
		} else {
			playerBehavior.PlayerCommandExplore ();
			isExploring = true;
		}
	}

}
