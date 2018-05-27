using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : ISingleton<DialogueManager> {
	
	protected DialogueManager() { }

    public bool isActive;

	private Actor actor;
	private Queue<string> sentences;
    private Actor player;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
        player = PlayerControler.Instance.GetComponent<Actor>();
	}

	private void Update()
	{
        if (TimeManager.Instance.gameIsPaused) return;
        if(isActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
            }
        }
	}

	public void StartDialogue(Actor newActor, string[] newSentences)
    {
        isActive = true;
        Debug.Log("sentence " + newSentences[0]);

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

        SoundManager.Instance.PlaySound("UI/DialogWindow");
		string sentence = sentences.Dequeue ();			//prend la prochaine phrase
		actor.dialogueBox.textMeshPro.text = sentence;	//affiche là
	}
		

	void EndDialogue(){
		actor.dialogueBox.Hide ();
        //SequenceManager.Instance.NextAction ();
        isActive = false;
	}

    public void PlayerSay(string dialogueName)
    {
        if(player.dialogues.Contains(dialogueName))
        {
            StartDialogue(player, (string[])player.dialogues[dialogueName]);
        }
        else
        {
            Debug.Log("this dialogue doesn't exist on this actor");
        }
    }

    public void ActorSay(Actor actor, string dialogueName)
    {
        if (actor.dialogues.Contains(dialogueName))
        {
            StartDialogue(actor, (string[])actor.dialogues[dialogueName]);
        }
        else
        {
            Debug.Log("this dialogue doesn't exist on this actor");
        }
    }

}
