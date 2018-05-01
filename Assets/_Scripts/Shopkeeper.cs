using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable {

    public override void Interact()
    {
        Actor actor = GetComponent<Actor>();
        DialogueManager.Instance.ActorSay(actor, "Quest");
    }
}
