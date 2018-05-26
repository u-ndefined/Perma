using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Motor))]
public class PlayerControler : ISingleton<PlayerControler>
{

    protected PlayerControler()
    {
    }

    public delegate void OnFocusChanged(Interactable newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public Interactable focus;  // Our current focus: Item, Enemy etc.

    public float rotationSpeed = 5f;

    private Motor motor;      // Reference to our motor
    private Camera cam;             // Reference to our camera
    private InventoryInputsHandler inventoryInputs;
    private Rigidbody rb;
    private InventoryManager inventory;
    [HideInInspector]
    public Animator animator;

    private bool isPressing = false;
    private Vector3 prevMousePos;
    private Vector3 inputDirection;

    // Get references
    void Start()
    {
        motor = GetComponent<Motor>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        inventory = InventoryManager.Instance;
        animator = GetComponentInChildren<Animator>();
        inventoryInputs = InventoryManager.Instance.GetComponent<InventoryInputsHandler>();
    }

	private void FixedUpdate()
	{
        if (DialogueManager.Instance.isActive)
        {
            animator.SetBool("Walk", false);
            return;
        }

        if(isPressing)
        {
            LeftClic();
        }

        if(motor.isWalking)rb.velocity = motor.direction * motor.moveSpeed;
        else rb.velocity = inputDirection * motor.moveSpeed;

        if(rb.velocity.normalized != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
        }
       
        if (inputDirection != Vector3.zero)
        {
            MovePlayer();
        }
        else if(!motor.isWalking) animator.SetBool("Walk", false);


	}

    private void MovePlayer()
    {
        SetFocus(null);  //stop following target if there is an input
        animator.SetBool("Walk", true);

        /*
        if (inputDirection != transform.forward) //face direction
        {
            Quaternion rotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        */


    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;

        }

        if (DialogueManager.Instance.isActive)
        {
            animator.SetBool("Walk", false);
            return;
        }

        //direction in the world
        inputDirection = GetMoveDirection();

        if (EventSystem.current.IsPointerOverGameObject() || inventoryInputs.dragging)
            return;
        

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            LeftClic();
            isPressing = true;
        }

        // If we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            if (!inventory.UseSlot())
            {
                return; //use slot, if nothing to use, do nothing else move to target
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();       // get interractable under mouse


                if (interactable != null && interactable.tag != "Player")
                {

                    SetFocus(interactable);                                 //go to the object
                }
                else
                {
                    inventory.ResetSlotUsed();                              //reset stack used if nothing to interact with
                }
            }
        }



    }

    // Set our focus to a new focus
    public void SetFocus(Interactable newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
        }

        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(transform);
        }

    }

    private Vector3 GetMoveDirection()
    {
        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //camera forward and right vectors:
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //direction in the world
        return (forward * verticalAxis + right * horizontalAxis).normalized;
    }

    private void LeftClic()
    {
        inventory.ResetSlotUsed();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))                         //if interactable follow it
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable is HexCell == false && interactable.tag != "Player")
            {
                Debug.Log("focus " + interactable.name);
                SetFocus(interactable);
            }
            else
            {
                motor.MoveToPoint(hit.point);                           //else go there
            }
        }
    }


}
/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Motor))]
public class PlayerControler : ISingleton<PlayerControler>
{

    protected PlayerControler()
    {
    }

    public LayerMask movementMask;
    private Camera cam;
    private Motor motor;
    public Interactable focus;
    private Rigidbody rb;
    public float rotationSpeed = 5f;
    private InventoryManager inventory;

    private bool isPressing = false;
    private Vector3 previousMousePos;

    //private AnimatorScript animator;
    public Animator animator;
    private InventoryInputsHandler inventoryInputs;



    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<Motor>();
        rb = GetComponent<Rigidbody>();
        inventory = InventoryManager.Instance;

        animator = GetComponentInChildren<Animator>();
        inventoryInputs = InventoryManager.Instance.GetComponent<InventoryInputsHandler>();
    }

    void FixedUpdate()
    {
        if (DialogueManager.Instance.isActive) return;
        MovePlayer();

    }

    private void MovePlayer()
    {
        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //camera forward and right vectors:
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //direction in the world
        Vector3 moveDirection = (forward * verticalAxis + right * horizontalAxis).normalized;

        //now we can apply the movement:
        rb.velocity = moveDirection * motor.moveSpeed;



        if (moveDirection != Vector3.zero)
        {
            RemoveFocus();  //stop following target if there is an input

            animator.SetBool("Walk", true);

            //animator.ChangeState(GameData.Animation.Walk);


            if (moveDirection != transform.forward) //face direction
            {
                Quaternion rotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
        else 
        {
            if(!motor.hasPath) animator.SetBool("Walk", false); //dzalfjazbmlfhamzlekfhmalzkefnmlajznefmjlnazemljnfzajenfkjaznefmlejnzflkazjneljfnalzkjefnazkjlenfkzjanfkjzanflkzjan
        }
    }


    void Update()
    {
        if (DialogueManager.Instance.isActive) return;
        if (EventSystem.current.IsPointerOverGameObject() || inventoryInputs.dragging)
        {               //return if mouse onUI
            return;
        }

        if (Input.GetMouseButtonDown(1))       //if right clic
        {
            if (!inventory.UseSlot())
            {
                return; //use slot, if nothing to use, do nothing else move to target
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();       // get interractable under mouse


                if (interactable != null)
                {
                    
                    SetFocus(interactable);                                 //go to the object
                }
                else
                {
                    inventory.ResetSlotUsed();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {                               //if left clic

            inventory.ResetSlotUsed();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))                         //if interactable follow it
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null && interactable is HexCell == false)
                {
                    Debug.Log("focus " + interactable.name);
                    SetFocus(interactable);
                }
                else
                {
                    motor.MoveToPoint(hit.point);                           //else go there
                }

                previousMousePos = hit.point;
                isPressing = true;

            }
        }

        if (isPressing)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))                         //if interactable follow it
            {

                if (Vector3.Distance(previousMousePos, hit.point) > 1)
                {
                    inventory.ResetSlotUsed();

                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null && interactable is HexCell == false)
                    {
                        Debug.Log(interactable.name);
                        SetFocus(interactable);
                    }
                    else
                    {
                        motor.MoveToPoint(hit.point);                           //else go there
                    }

                    previousMousePos = hit.point;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }



    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus.transform);
        }
        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {

        if (focus != null)
        {
            inventory.ResetSlotUsed();
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
*/