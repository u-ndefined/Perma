using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRoutine : MonoBehaviour 
{
    private Actor actor;
    public Flag[] flags;
    public int step = -1;
    public bool waitNextDay = false;

	private void Start()
	{
        actor = GetComponent<Actor>();
        SortFlags();
        NextFlag();
        TimeManager.Instance.OnNewDayEvent += NextDay;
	}

	private void Update()
	{
        if(!waitNextDay && TimeManager.Instance.clock > flags[step].clock)
        {
            NextFlag();
        }
	}

    private void NextDay()
    {
        waitNextDay = false;
    }

    private void NextFlag()
    {
        step++;
        if (step >= flags.Length)
        {
            step = 0;
            waitNextDay = true;
        }
        Vector3 pos = flags[step].target.position;
        actor.motor.MoveToPoint(pos);

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
