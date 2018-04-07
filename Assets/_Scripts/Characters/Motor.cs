﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Motor : MonoBehaviour {

	public delegate void OnPointReached();
	public OnPointReached onPointReached;

	private NavMeshAgent agent;

	private Transform target;
	private Vector3 pointToFace;
	private Vector3 previousTargetPosition;

	private Queue<Vector3> cornerQueue;
	public float stoppingDistance = 1f;
	private float currentStoppingDistance = 1f;
	public float moveSpeed = 1f;
	private Vector3 currentDestination;
	private float currentDistance;
	private Vector3 direction;
    private bool hasPath;
	private bool isFollowing = false;

	void Start(){
		agent = GetComponent<NavMeshAgent> ();

		agent.enabled = false;
		cornerQueue = new Queue<Vector3>();
	}

	void Update(){
		if (isFollowing && previousTargetPosition != target.position) {
			previousTargetPosition = target.position;
			MoveToPoint (target.position);
		}
		MoveAlongPath ();
	}

	public void MoveToPoint(Vector3 point){
		agent.enabled = true;
		agent.SetDestination (point);
		SetupPath(agent.path);
		agent.enabled = false;
	}

	public void FollowTarget(Interactable newTarget){
		currentStoppingDistance = newTarget.radius * 0.85f;
		isFollowing = true;
		target = newTarget.transform;
		previousTargetPosition = target.position;
		MoveToPoint (target.position);
	}
	public void StopFollowingTarget(){
        Debug.Log("Stop!");
		currentStoppingDistance = stoppingDistance;
		isFollowing = false;
		target = null;
        hasPath = false;
	}


	private void SetupPath(NavMeshPath path){
		if (cornerQueue.Count != 0) {
			cornerQueue.Clear ();
		}

		foreach(Vector3 corner in path.corners)
		{
			cornerQueue.Enqueue(corner);
		}

		GetNextCorner ();
	}

	private void GetNextCorner()
	{
		if(cornerQueue.Count > 0)
		{
			currentDestination = cornerQueue.Dequeue();
			pointToFace = currentDestination;
			direction = (currentDestination - transform.position).normalized;
			hasPath = true;

		}
		else
		{
			hasPath = false;

			if (onPointReached != null) { 
				StopFollowingTarget ();
				onPointReached.Invoke ();
			}
		}
	}

	private void MoveAlongPath()
	{
		if(hasPath)
		{
			
			currentDistance = (currentDestination - transform.position).sqrMagnitude;

			if(currentDistance > currentStoppingDistance)
			{
                //maybe use velocity on rb?
				transform.position +=  direction  * moveSpeed * Time.deltaTime;
                FaceTarget();
			}
			else
			{
				GetNextCorner();
			}
		}
	}

	void FaceTarget(){
		Vector3 dir = (pointToFace - transform.position).normalized;
		dir = new Vector3(dir.x,0f,dir.z);
		if (dir != Vector3.zero) {
			Quaternion lookRotation = Quaternion.LookRotation (dir);
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
		}
	}


}