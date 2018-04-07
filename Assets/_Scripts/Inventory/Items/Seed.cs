using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New seed", menuName = "Items/Seed")]
[System.Serializable]
public class Seed : Item {

    public Mesh[] growthSteps;

    public override void Use()
    {
        base.Use();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                PlayerControler.Instance.SetFocus(interactable);
            }
        }


    }
}
