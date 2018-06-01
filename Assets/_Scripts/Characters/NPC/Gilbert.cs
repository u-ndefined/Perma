using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gilbert : MonoBehaviour 
{
    private NPCRoutine routine;
    private NPC npc;
    public float endTimerMinute;
    private float endTimerSecond;
    private float start;
    private bool wait = true;

	// Use this for initialization
	void Awake () 
    {
        routine = GetComponent<NPCRoutine>();
        npc = GetComponent<NPC>();
        routine.isActive = false;
        endTimerSecond = endTimerMinute * 60f;
        start = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Time.time - start >= endTimerSecond)
        {
            routine.isActive = false;
            routine.actor.motor.OnFocusChanged(PlayerControler.Instance.GetComponent<Interactable>());
            npc.end = true;
        }
        if(npc.end)
        {
            float distance = (PlayerControler.Instance.transform.position - transform.position).sqrMagnitude;
            if (distance <= Mathf.Pow(routine.actor.motor.agent.stoppingDistance, 2))
            {
                npc.Interact();
            }
        }
	}

	private void OnTriggerEnter(Collider other)
	{
        if(wait && other.CompareTag("Player"))
        {
            wait = false;
            routine.isActive = true;
            npc.Interact();
            GetComponentInChildren<BoxCollider>().gameObject.SetActive(false);
        }
	}
}
