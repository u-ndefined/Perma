
using UnityEngine;

public class Interactable : MonoBehaviour {

    public delegate void voidNoParam();
    public voidNoParam onActionDoneEvent;

	public float radius = 3f;

	private bool isFocus = false;
	Transform player;

	private bool hasInteracted = false;

	public Transform interactionTransform;

    private AnimatorScript animator;

    private bool isActing = false;
    private float actionTimer;

	private void Start()
	{
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }

	}


	void Update(){
		if (isFocus && !hasInteracted) {
			float distance = Vector3.Distance (player.position, interactionTransform.position);
			if (distance < radius) 
            {
                if (!InventoryManager.Instance.stackUsed.empty) UseObjectOn(InventoryManager.Instance.stackUsed);
				else Interact ();
				hasInteracted = true;
			}
		}

        if(isActing && actionTimer < Time.time)
        {
            if (onActionDoneEvent != null)
                onActionDoneEvent = null;
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

    public void DoAction(GameData.Animation animation, float second, Clock inGameTime)
    {
        PlayerControler.Instance.animator.SetTrigger(animation.ToString());
        isActing = true;
        actionTimer = Time.time + second;
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
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused(){
		isFocus = false;
		player = null;
		hasInteracted = false;
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
