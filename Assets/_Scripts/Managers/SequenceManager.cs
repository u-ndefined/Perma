using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : ISingleton<SequenceManager> {
	protected SequenceManager() { }

	private Queue<Action> actions;
	private Actor actor;

	void Start () {
		actions = new Queue<Action>();
	}

	public void StartSequence(Sequence sequence){
		//actions.Clear ();
		foreach (Action action in sequence.actions) {
			actions.Enqueue (action);
		}
		NextAction ();
	}

	public void NextAction(){
		if (actions.Count == 0) {						//s'il n'y a plus de phrases fini le dialogue
			EndSequence ();
			return;
		}
		Action action = actions.Dequeue ();
		actor = action.actor;
		switch (action.actionType) {
		case ActionType.Say:
			DialogueManager.Instance.StartDialogue(action.actor, action.sentences);
			break;
		case ActionType.Move:
			action.actor.motor.MoveToPoint (action.targetPosition);
			action.actor.motor.onPointReached += PointReached;
			break;
		case ActionType.Follow:
			action.actor.motor.FollowTarget (action.target);
			action.actor.motor.onPointReached += PointReached;
			break;
		}
	}

	void PointReached( ){
		actor.motor.onPointReached -= PointReached;
		actor.motor.StopFollowingTarget ();
		NextAction ();
	}

	void EndSequence(){
	}
}
