using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routine : MonoBehaviour
{
    public RoutineState[] routineStates;

	private void Start()
	{
        routineStates[0].actions[0].Act(transform);
	}
}
