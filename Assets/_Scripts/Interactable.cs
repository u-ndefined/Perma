
using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 3f;

	private bool isFocus = false;
	Transform player;

	private bool hasInteracted = false;

	public Transform interactionTransform;

	private void Start()
	{
        if(interactionTransform == null)
        {
            Debug.Log("le gros bug");
            interactionTransform = transform;
        }
	}


	void Update(){
		if (isFocus && !hasInteracted) {
			float distance = Vector3.Distance (player.position, interactionTransform.position);
			if (distance < radius) {
				Interact ();
				hasInteracted = true;
			}
		}
	}

	public virtual void Interact(){
		Debug.Log(name + " interacting with " + player.name);
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
