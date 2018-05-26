using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* This component moves our player.
        - If we have a focus move towards that.
        - If we don't move to the desired point.
*/

public class Motor : MonoBehaviour
{
    public float moveSpeed;
    public Transform target;
    public NavMeshAgent agent;     // Reference to our NavMeshAgent
    private PlayerControler player;
    private Animator animator;
    public bool isWalking = false;
    private float stoppingDistance;
    private bool searchingPath = false;
    private Queue<Vector3> cornerQueue;
    private Vector3 currentDestination;
    public Vector3 direction;
    public bool hasPath = false;
    private Rigidbody rb;
    private Vector3 previousPosition;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        stoppingDistance = agent.stoppingDistance;

        moveSpeed = agent.speed;

        agent.speed = 0;

        cornerQueue = new Queue<Vector3>();

        player = GetComponent<PlayerControler>();

        if(player != null)
        {
            player.onFocusChangedCallback += OnFocusChanged;

        }

    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        animator.SetBool("Walk", true);
        isWalking = true;
        searchingPath = true;
    }

    public void OnFocusChanged(Interactable newFocus)
    {
        if (newFocus != null)
        {
            agent.stoppingDistance = newFocus.radius * 0.9f;
            target = newFocus.interactionTransform;
            previousPosition = target.position;
            MoveToPoint(target.position);
        }
        else
        {
            agent.stoppingDistance = stoppingDistance;
            target = null;
            //agent.isStopped = true;
            //agent.ResetPath();
            hasPath = false;
        }
    }

    void Update()
    {

        if (hasPath)
        {
            direction = (currentDestination - transform.position).normalized;
            if(Vector3.Distance(currentDestination, transform.position) <= 0.1f) GetNextCorner();
        }
        else
        {
            isWalking = false;
        }

        if (searchingPath && !agent.pathPending)
        {
            searchingPath = false;
            SetupPath(agent.path);
            Debug.Log("setup");
        }
        
        //if (!agent.pathPending)
        //{
        //    if (agent.remainingDistance <= agent.stoppingDistance)
        //    {
        //        if (!player)
        //        {
        //            animator.SetBool("Walk", false);
        //            //OnFocusChanged(null);
        //        }
        //        isWalking = false;
        //        //agent.isStopped = true;
        //        //agent.ResetPath();
        //    }
        //}

    }

	private void FixedUpdate()
	{

        if (target != null && target.position != previousPosition)
        {
            MoveToPoint(target.position);
            previousPosition = target.position;
            //FaceTarget();
        }
	}

	//void FaceTarget()
    //{
    //    Vector3 direction = (target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //}

    private void SetupPath(NavMeshPath path)
    {
        if (cornerQueue.Count != 0)
        {
            cornerQueue.Clear();
        }

        foreach (Vector3 corner in path.corners)
        {
            cornerQueue.Enqueue(corner);
        }
        Debug.Log(cornerQueue.Dequeue());
        Debug.Log(cornerQueue.Count);
        GetNextCorner();
    }

    private void GetNextCorner()
    {
        if (cornerQueue.Count > 0)
        {
            currentDestination = cornerQueue.Dequeue();
            hasPath = true;
        }
        else
        {
            hasPath = false;
        }
    }



}
/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Motor : MonoBehaviour {

    public delegate void OnPointReached();
    public OnPointReached onPointReached;

    private NavMeshAgent agent;
    private Rigidbody rb;

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
    public bool hasPath;
    private bool isFollowing = false;
    private bool searchingPath = false;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent> ();

        agent.enabled = false;
        cornerQueue = new Queue<Vector3>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
            
        if (searchingPath && !agent.pathPending)
        {
            searchingPath = false;
            SetupPath(agent.path);
            agent.enabled = false;
        }

        if (isFollowing && Vector3.Distance(previousTargetPosition, target.position) > 1f)
        {
            previousTargetPosition = target.position;
            MoveToPoint(target.position);
        }

        
    }

    private void FixedUpdate()
    {
        MoveAlongPath();
    }



    public void MoveToPoint(Vector3 point)
    {
        agent.enabled = true;
        searchingPath = true;
        agent.SetDestination(point);
    }

    public void FollowTarget(Transform newTarget)
    {
        Interactable interactable = newTarget.GetComponent<Interactable>();

        if(interactable != null)
        {
            currentStoppingDistance = interactable.radius * 0.85f;
        }

       
        isFollowing = true;
        target = newTarget.transform;
        previousTargetPosition = target.position;
        MoveToPoint(target.position);
    }

    public void StopFollowingTarget(){
        
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
            animator.SetBool("Walk", true);
            currentDestination = cornerQueue.Dequeue();
            pointToFace = currentDestination;
            direction = (currentDestination - transform.position).normalized;
            hasPath = true;

        }
        else
        {
            hasPath = false;

            animator.SetBool("Walk", false);

            if (onPointReached != null) 
            { 
                StopFollowingTarget ();
                onPointReached.Invoke ();
            }
        }
    }

    private void MoveAlongPath()
    {
        if(transform.position )
        if(hasPath)
        {
            
            currentDistance = (currentDestination - transform.position).sqrMagnitude;

            if(currentDistance > 0.1f)
            {
                
                rb.AddForce(direction * moveSpeed * Time.timeScale, ForceMode.VelocityChange);
                //Debug.Log(direction * moveSpeed);
                //rb.velocity = direction * moveSpeed;
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
*/