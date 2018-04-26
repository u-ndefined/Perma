using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTrigger : Interactable {

	public Sequence[] randomSequences;

	public override void Interact(){
		TriggerRandomSequences ();
	}

	void TriggerRandomSequences(){
		int random = Random.Range (0, randomSequences.Length);
		//SequenceManager.Instance.StartSequence (randomSequences[random]);
	}
}
