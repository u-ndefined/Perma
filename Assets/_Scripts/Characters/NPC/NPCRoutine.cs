using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRoutine : MonoBehaviour 
{
    public Actor actor;
    public Flag[] flags;
    private int step;
    private bool waitNextDay = false;
    [HideInInspector]
    public bool isActive = false;

	private void Start()
	{
        SortFlags();
        CheckFlag();
        TimeManager.Instance.OnNewDayEvent += NextDay;
	}

	private void Update()
	{
        if (!isActive) return;
        if(!waitNextDay && TimeManager.Instance.clock > flags[step].clock)
        {
            CheckFlag();
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
