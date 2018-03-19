using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Motor : MonoBehaviour {

	public delegate void OnPointReached();
	public OnPointReached onPointReached;

	private NavMeshAgent agent;
	private NavMeshObstacle obstacle;

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
		obstacle = GetComponent<NavMeshObstacle> ();

		agent.enabled = false;
		obstacle.enabled = true;
		cornerQueue = new Queue<Vector3>();
	}

	void Update(){
		if (isFollowing && previousTargetPosition != target.position) {
			previousTargetPosition = target.position;
			MoveToPoint (target.position);
		}
		MoveAlongPath ();
		FaceTarget ();
	}

	public void MoveToPoint(Vector3 point){
		obstacle.enabled = false;
		agent.enabled = true;
		agent.SetDestination (point);
		SetupPath(agent.path);
		agent.enabled = false;
		obstacle.enabled = true;
	}

	public void FollowTarget(Interactable newTarget){
		currentStoppingDistance = newTarget.radius * 0.85f;
		isFollowing = true;
		target = newTarget.transform;
		previousTargetPosition = target.position;
		MoveToPoint (target.position);
	}
	public void StopFollowingTarget(){
		currentStoppingDistance = stoppingDistance;
		isFollowing = false;
		target = null;
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
				transform.position +=  direction  * moveSpeed * Time.deltaTime;
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





















/*
[RequireComponent(typeof(NavMeshAgent))]
public class Motor : MonoBehaviour {

	Transform target;
	Vector3 previousTargetPosition;
	NavMeshAgent agent;

	public delegate void OnPointReached();
	public OnPointReached onPointReached;

	public Queue<Vector3> cornerQueue;
	public Vector3 currentDestination;
	bool hasPath;
	public float currentDistance;
	public float moveSpeed;
	Vector3 direction;
	public float minDistanceArrived = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		cornerQueue = new Queue<Vector3>();
	}

	// Update is called once per frame
	void Update () {
		MoveAlongPath ();
		//mieux de faire une coroutine
		if (target != null) {
			FaceTarget ();
			if(previousTargetPosition != target.position){
				SetDestination (target.position);
			}
		}
	}

	public void MoveToPoint(Vector3 point){
		SetDestination (point);
	}

	public void FollowTarget(Interactable newTarget){
		agent.stoppingDistance = newTarget.radius * 0.85f;
		agent.updateRotation = false;
		target = newTarget.transform;
		
	}

	public void StopFollowingTarget(){
		agent.stoppingDistance = 0f;
		agent.updateRotation = true;
		target = null;
	}

	void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0f,direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void GetNextCorner()
	{
		if(cornerQueue.Count > 0)
		{
			currentDestination = cornerQueue.Dequeue();
			Debug.Log (currentDestination);
			direction = transform.position - currentDestination;
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

	void MoveAlongPath()
	{
		if(hasPath)
		{
			Debug.Log ("hasPath");

			currentDistance = (transform.position - currentDestination).sqrMagnitude;
			Debug.Log ("Dist" + currentDistance);

			if(currentDistance > minDistanceArrived)
			{
				Debug.Log ("bouge");
				transform.position +=  direction  * moveSpeed * Time.deltaTime;
			}
			else
			{
				GetNextCorner();
			}
		}
	}

	void SetDestination(Vector3 destination){
		agent.enabled = true;
		agent.SetDestination (destination);
		SetupPath (agent.path);
		agent.enabled = false;
	}

	void SetupPath(NavMeshPath path)
	{
		if (cornerQueue.Count != 0) {
			Debug.Log ("clear");
			cornerQueue.Clear ();
		}

		foreach(Vector3 corner in path.corners)
		{
			cornerQueue.Enqueue(corner);
		}
			
		//currentDistance = (transform.position - currentDestination).sqrMagnitude;
		//Debug.Log ("dist" + currentDistance);
		//hasPath = true;
		GetNextCorner();
	}

	
	private bool PointReached(){ 			//retroune vrai si l'agent est arrivé à destination
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				return true;
			}
		}
		return false;
	}

}
*/
