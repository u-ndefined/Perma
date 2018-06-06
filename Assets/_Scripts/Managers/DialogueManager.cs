using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : ISingleton<DialogueManager> {
	
	protected DialogueManager() { }

    public bool isActive;

    public Actor actor;
	private Queue<string> sentences;
    private Actor player;

    public bool end = false;

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
        if (end)
        {
            Fade.Instance.FadeOut(true, 5);
            Fade.Instance.onFadeEndEvent += Quit;
        }
       
         PlayerControler.Instance.actionInProgress = false;

        if(end)
        {
            PlayerControler.Instance.actionInProgress = true;
        }
	}

	private void Quit()
	{
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program 
        Application.Quit();
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
