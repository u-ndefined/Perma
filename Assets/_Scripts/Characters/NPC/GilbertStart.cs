using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GilbertStart : MonoBehaviour {

    private NPCRoutine routine;
    private bool done = false;
    public bool isChecked = false;

	private void Start()
	{
        routine = GetComponent<NPCRoutine>();
        routine.isActive = false;
        routine.actor.motor.OnFocusChanged(PlayerControler.Instance.GetComponent<Interactable>());
	}

	private void Update()
	{
        if (done && !isChecked)
        {
            if (!DialogueManager.Instance.isActive)
            {
                routine.isActive = true;
                routine.CheckFlag();
                isChecked = true;
            }
        }

        if(!done)
        {
            Debug.Log(routine.actor.motor.isWalking);
            if(routine.actor.motor.isWalking == false)
            {
                done = true;

                GetComponent<NPC>().Interact();
            }
        }


	}
}
