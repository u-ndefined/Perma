﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : ISingleton<DialogueManager> {
	
	protected DialogueManager() { }

	private Actor actor;
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Actor newActor, string[] newSentences){
		actor = newActor;
		actor.dialogueBox.Display ();					//affiche la dialogueBox
		sentences.Clear ();
		foreach(string sentence in newSentences){		//stock toutes les phrases
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();							//affiche la prochaine
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {						//s'il n'y a plus de phrases fini le dialogue
			EndDialogue ();
			return;
		}
		string sentence = sentences.Dequeue ();			//prend la prochaine phrase
		actor.dialogueBox.textMeshPro.text = sentence;	//affiche là
	}
		

	void EndDialogue(){
		actor.dialogueBox.Hide ();
		SequenceManager.Instance.NextAction ();
	}

}
