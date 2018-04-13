using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Motor))]
public class PlayerControler : ISingleton<PlayerControler> {

    protected PlayerControler() { }

	public LayerMask movementMask;
	private Camera cam;
	private Motor motor;
	public Interactable focus;
    private Rigidbody rb;
    public float rotationSpeed = 5f;





	void Start () 
    {
		cam = Camera.main;
		motor = GetComponent<Motor> ();
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        
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

            if (moveDirection != transform.forward) //face direction
            {
                Quaternion rotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
	

	void Update () 
    {
        

		if (EventSystem.current.IsPointerOverGameObject ()) 
        {               //return if mouse onUI
			return;
		}

        if (Input.GetMouseButtonDown (0))       //if left clic
        {                                 


            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();       // get interractable under mouse

                if (interactable != null)
                {
                    SetFocus(interactable);                                 //go to the object
                }
            }

            InventoryManager.Instance.UseSlot();                            //use active slot
           
		}

        if (Input.GetMouseButtonDown (1)) {                                 //if right clic
            
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit,100))                         //if interactable follow it
            {                 
				Interactable interactable = hit.collider.GetComponent<Interactable> ();

                if(interactable != null && interactable is HexCell == false){
					SetFocus (interactable);
				}
                else
                {
                    motor.MoveToPoint(hit.point);                           //else go there
                }


				
			}
			
		}
		

	}

	public void SetFocus(Interactable newFocus){
		if (newFocus != focus) {
			if (focus != null) {
				focus.OnDefocused ();
			}
			focus = newFocus;
			motor.FollowTarget (newFocus);
		}
		newFocus.OnFocused (transform);
	}

	public void RemoveFocus(){
		if (focus != null) {
			focus.OnDefocused ();
		}
		focus = null;
		motor.StopFollowingTarget ();
	}
}
