﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHouse : Interactable 
{
    public ScriptableDialogues nightDialog;
    public ScriptableDialogues dayDialog;
    private NPCRoutine npc;
    public bool waitNextDay = false;
    private bool isHiding;
    public Clock clock;


    protected override void Start()
	{
        base.Start();
        TimeManager.Instance.OnNewDayEvent += NextDay;
	}


	public override void Interact()
	{
        base.Interact();

        if(isHiding) DialogueManager.Instance.PlayerSay(nightDialog.dialogueName);
        else DialogueManager.Instance.PlayerSay(dayDialog.dialogueName);

        
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "NPC" && !isHiding)
        {
            npc = other.GetComponent<NPCRoutine>();
            if(npc != null && npc.hide && !npc.dial)
            {
                Hide();
            }
        }
	}

    protected override void Update()
    {
        base.Update();
        if (isHiding && !waitNextDay && TimeManager.Instance.clock > clock)
        {
            Display();
        }
    }

    private void Hide()
    {
        isHiding = true;
        clock = npc.flags[npc.step].clock;
        waitNextDay = npc.waitNextDay;
        npc.gameObject.SetActive(false);
    }
    private void Display()
    {
        isHiding = false;
        npc.gameObject.SetActive(true);
        npc.CheckFlag();
    }
    private void NextDay()
    {
        waitNextDay = false;
    }
}
