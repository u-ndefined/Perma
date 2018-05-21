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
        dialogueBox = GetComponentInChildren<DialogueBox>(true);
        dialogueBox.GetComponent<FaceCamera>().character = transform;

        dialogues = new Hashtable();

        for (int i = 0; i < dialoguesInInspector.Length; i++)
        {
            ScriptableDialogues dialogue = dialoguesInInspector[i];
            dialogues.Add(dialogue.dialogueName, dialogue.sentences);
        }

	}
}
