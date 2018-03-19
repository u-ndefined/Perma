using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Motor))]
public class PlayerControler : MonoBehaviour {

	public LayerMask movementMask;
	private Camera cam;
	private Motor motor;
	public Interactable focus;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		motor = GetComponent<Motor> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			

			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit,100,movementMask)){

				motor.MoveToPoint(hit.point);

				RemoveFocus ();
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit,100)){
				Interactable interactable = hit.collider.GetComponent<Interactable> ();
				if(interactable != null){
					SetFocus (interactable);
				}


				motor.MoveToPoint(hit.point);
			}
		}
	}

	void SetFocus(Interactable newFocus){
		if (newFocus != focus) {
			if (focus != null) {
				focus.OnDefocused ();
			}
			focus = newFocus;
			motor.FollowTarget (newFocus);
		}
		newFocus.OnFocused (transform);
	}
	void RemoveFocus(){
		if (focus != null) {
			focus.OnDefocused ();
		}
		focus = null;
		motor.StopFollowingTarget ();
	}
}
