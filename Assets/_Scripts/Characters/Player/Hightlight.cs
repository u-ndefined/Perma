using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hightlight : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;
    private Camera cam;
    public Transform HexHightlighter;

	private void Start()
	{
        cam = Camera.main;
	}


	private void FixedUpdate()
	{
        if (EventSystem.current.IsPointerOverGameObject())
        {    
            HexHightlighter.gameObject.SetActive(false);
            return;
        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            HexHightlighter.gameObject.SetActive(false);

            if (interactable != null)
            {
                if(interactable is HexCell)
                {
                    if(((HexCell)interactable).isActive && ((HexCell)interactable).plant == null)
                    {
                        HexHightlighter.gameObject.SetActive(true);
                        HexHightlighter.position = hit.transform.position;
                    }
                   
                }
            }

        }
	}
}
