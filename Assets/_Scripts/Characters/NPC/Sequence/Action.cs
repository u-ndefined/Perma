using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {
	Say,
	Move,
	Follow,
	Play
}

[System.Serializable]
public class Action {

	public Actor actor;
	public ActionType actionType;

	[TextArea(3,20)]
	public string[] sentences;

	public Vector3 targetPosition;

	public Interactable target;
}
