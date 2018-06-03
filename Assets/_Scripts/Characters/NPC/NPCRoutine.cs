using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRoutine : MonoBehaviour 
{
    public Actor actor;
    public Flag[] flags;
    public int step;
    //[HideInInspector]
    public bool waitNextDay = false;
    public bool isActive = true;
    private Rigidbody rb;
    private Animator animator;
    public float rotationSpeed = 0.125f;
    [HideInInspector]
    public bool hide;
    [HideInInspector]
    public bool dial;


	private void Start()
	{
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SortFlags();
        if (isActive)CheckFlag();
        TimeManager.Instance.OnNewDayEvent += NextDay;

	}




	private void Update()
	{
        if (!isActive) return;
        if (DialogueManager.Instance.isActive && DialogueManager.Instance.actor == actor) 
        {
            animator.SetBool("Walk", false);
            Quaternion rotation = Quaternion.LookRotation(PlayerControler.Instance.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
            actor.motor.agent.isStopped = true;
            dial = true;
            return;
        }
        if(dial)
        {
            dial = false;
            actor.motor.OnFocusChanged(null);
            CheckFlag();
        }
        else if(!waitNextDay && TimeManager.Instance.clock > flags[step].clock)
        {
            CheckFlag();
        }

	}

	private void FixedUpdate()
	{
        /*
        rb.velocity = actor.motor.direction * actor.motor.moveSpeed;
        if (actor.motor.isWalking && rb.velocity.normalized != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
        }
        */
	}

	private void NextDay()
    {
        waitNextDay = false;
    }

    public void CheckFlag()
    {
        step = -1;
        for (int i = flags.Length - 1; i >= 0; i--)
        {
            if (flags[i].clock > TimeManager.Instance.clock) step = i;
            else break;
        }
        Debug.Log(step);
        if (step == -1)
        {
           
                step = 0;
                waitNextDay = true;

        }
        hide = flags[step].hide;
        Interactable interactable = flags[step].target.GetComponent<Interactable>();
        if (interactable != null)
        {
            actor.motor.OnFocusChanged(interactable);
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
