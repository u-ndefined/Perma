using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : Interactable {

    public float fadeOutDuration;
    public float fadeInDuration;
    public Clock wakeUpClock;
    private Fade fade;
    public Transform playerStart;

    protected override void Start()
	{
        base.Start();
        fade = Fade.Instance;
        //playerStart = GetComponentInChildren<Transform>();
	}



	public override void Interact()
    {
        fade.FadeOut(true, fadeOutDuration);
        PlayerControler.Instance.animator.SetBool("Walk", false);
        PlayerControler.Instance.actionInProgress = true;
        SoundManager.Instance.PlaySound("PlayerAction/SleepingMusic");
        fade.onFadeEndEvent += GoToBed;

    }

    private void GoToBed()
    {
        TimeManager.Instance.clock = wakeUpClock;
        TimeManager.Instance.NextDay();
        Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
        PlayerControler.Instance.transform.rotation = rotation;
        Debug.Log(playerStart.position);
        PlayerControler.Instance.transform.position = playerStart.position;
        fade.FadeOut(false, fadeInDuration);
        fade.onFadeEndEvent -= GoToBed;
        fade.onFadeEndEvent += WakeUp;
    }

    private void WakeUp()
    {
        DialogueManager.Instance.PlayerSay("NewDay");
        PlayerControler.Instance.actionInProgress = false;
        fade.onFadeEndEvent -= WakeUp;
    }
}
