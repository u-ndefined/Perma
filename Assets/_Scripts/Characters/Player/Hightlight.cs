using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if(interactable is HexCell)
                {
                    if(((HexCell)interactable).isActive)
                    {
                        HexHightlighter.gameObject.SetActive(true);
                        HexHightlighter.position = hit.transform.position;
                    }
                   
                }
                else
                {
                    HexHightlighter.gameObject.SetActive(false);
                }
            }

        }
	}
}
