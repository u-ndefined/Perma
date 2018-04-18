using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	public DialogueBox dialogueBox;
	public Motor motor;
    public ScriptableDialogues[] dialoguesInInspector;

    public Hashtable dialogues;

	private void Start()
	{
        dialogues = new Hashtable();

        for (int i = 0; i < dialoguesInInspector.Length; i++)
        {
            ScriptableDialogues dialogue = dialoguesInInspector[i];
            dialogues.Add(dialogue.dialogueName, dialogue.sentences);
        }
	}
}
