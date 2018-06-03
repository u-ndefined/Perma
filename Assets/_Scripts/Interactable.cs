
using UnityEngine;

public class Interactable : MonoBehaviour {

    public delegate void voidNoParam();
    public voidNoParam onActionDoneEvent;

	public float radius = 3f;

	private bool isFocus = false;
	Transform player;

    public bool hasInteracted = false;

	public Transform interactionTransform;


    private bool isActing = false;
    private float actionTimer;

    protected virtual void Start()
	{
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }

	}


    protected virtual void Update()
    {
        if (isFocus)    // If currently being focused
        {

            float distance = Vector3.Distance(player.position, interactionTransform.position);
            // If we haven't already interacted and the player is close enough
            if (!hasInteracted && distance <= radius && !PlayerControler.Instance.actionInProgress)
            {
                // Interact with the object
                hasInteracted = true;
                if (!InventoryManager.Instance.stackUsed.empty) UseObjectOn(InventoryManager.Instance.stackUsed);
                else Interact();
                PlayerControler.Instance.SetFocus(null);
            }
        }

        if(isActing)
        {
            player = PlayerControler.Instance.transform;
            Quaternion rotation = Quaternion.LookRotation(transform.position - player.position);
            player.rotation = Quaternion.Lerp(player.rotation, rotation, PlayerControler.Instance.rotationSpeed);
        }

        if (isActing && actionTimer < Time.time)
        {
            if (onActionDoneEvent != null)
            {
                onActionDoneEvent.Invoke();
                onActionDoneEvent = null;   
            }
            isActing = false;

            PlayerControler.Instance.actionInProgress = false;
        }
	}


	public virtual void Interact(){
		Debug.Log(name + " interacting with " + player.name);
	}

    public virtual void UseObjectOn(Stack stackUsedOn)
    {
        Debug.Log(stackUsedOn.item.name + " used on " + name);
        InventoryManager.Instance.ResetSlotUsed();
    }

    public void DoAction(Anim anim)
    {
        PlayerControler.Instance.animator.SetTrigger(anim.animation.ToString());
        isActing = true;
        TimeManager.Instance.clock += anim.inGameTime;
        actionTimer = Time.time + anim.realtime;
        PlayerControler.Instance.actionInProgress = true;
    }

    private void ActionDone()
    {
        if (onActionDoneEvent != null)         //update selector
        {
            onActionDoneEvent.Invoke();
        }
    }

	public void OnFocused(Transform playerTransform){
        isFocus = true;
        //hasInteracted = false;
        player = playerTransform;
	}

	public void OnDefocused(){
        isFocus = false;
        hasInteracted = false;
        player = null;
	}

	void OnDrawGizmosSelected(){
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (interactionTransform.position, radius);
	}
}