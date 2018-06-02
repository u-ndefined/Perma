using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour 
{
    public bool isWalking;
    public Vector3 direction;
    public float moveSpeed;
    private float stoppingDistance;
    private Vector3 destination;
    private Vector3 previousPosition;
    private Transform target;
    private Animator animator;
    PlayerControler player;

	// Use this for initialization
	void Awake () 
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerControler>();

	}

	private void OnEnable()
	{
        player.onFocusChangedCallback += OnFocusChanged;
	}

	private void OnDisable()
	{
        player.onFocusChangedCallback -= OnFocusChanged;
	}

	// Update is called once per frame
	void Update () 
    {
        if(isWalking)
        {
            if((destination - transform.position).sqrMagnitude <= 0.1f)
            {
                isWalking = false;
            }
            else
            {
                direction = (destination - transform.position).normalized;
            }
        }
	}

    public void MoveToPoint(Vector3 point)
    {
        destination = point; 
        animator.SetBool("Walk", true);
        isWalking = true;
    }

    public void OnFocusChanged(Interactable newFocus)
    {
        if (newFocus != null)
        {
            stoppingDistance = newFocus.radius * 0.9f;
            target = newFocus.interactionTransform;
            previousPosition = target.position;
            MoveToPoint(target.position);
        }
        else
        {
            stoppingDistance = 0.1f;
            target = null;

            isWalking = false;
        }
    }

    private void FixedUpdate()
    {

        if (target != null && target.position != previousPosition)
        {
            MoveToPoint(target.position);
            previousPosition = target.position;
        }
    }
}
