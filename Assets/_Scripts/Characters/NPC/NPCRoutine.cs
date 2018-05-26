﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRoutine : MonoBehaviour 
{
    public Actor actor;
    public Flag[] flags;
    private int step;
    private bool waitNextDay = false;
    public bool isActive = true;
    private Rigidbody rb;
    private Animator animator;
    public float rotationSpeed = 0.125f;

	private void Start()
	{
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SortFlags();
        CheckFlag();
        TimeManager.Instance.OnNewDayEvent += NextDay;
	}

	private void Update()
	{
        if (!isActive || DialogueManager.Instance.isActive) 
        {
            animator.SetBool("Walk", false);
            Quaternion rotation = Quaternion.LookRotation(PlayerControler.Instance.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
            rb.velocity = Vector3.zero;
            return;
        }
        if(!waitNextDay && TimeManager.Instance.clock > flags[step].clock)
        {
            CheckFlag();
        }
        rb.velocity = actor.motor.direction * actor.motor.moveSpeed;
        if (rb.velocity.normalized != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
        }
	}

    private void NextDay()
    {
        waitNextDay = false;
    }

    public void CheckFlag()
    {
        step = 0;
        for (int i = 0; i < flags.Length; i++)
        {
            if (flags[i].clock > TimeManager.Instance.clock) step = i;
            else break;
        }
        if (step == flags.Length - 1)
        {
            if(flags[step].clock < TimeManager.Instance.clock)
            {
                step = 0;
                waitNextDay = true;
            }

        }
        //if(flags[step].roam)
        Interactable interactable = flags[step].target.GetComponent<Interactable>();
        if (interactable != null)
        {
            actor.motor.OnFocusChanged(flags[step].target.GetComponent<Interactable>());
        }
        else
        {
            Vector3 pos = flags[step].target.position;
            actor.motor.MoveToPoint(pos);
        }


    }

	private void SortFlags()
    {
        for (int i = 0; i < flags.Length; ++i)
        {

            for (int j = i + 1; j < flags.Length; ++j)
            {

                if (flags[i].clock > flags[j].clock)
                {

                    Flag tempFlag = flags[i];
                    flags[i] = flags[j];
                    flags[j] = tempFlag;

                }

            }

        }
    }
}
