using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Interactable {
    public override void Interact()
    {
        SoundManager.Instance.PlaySound("PlayerAction/ClickHouseDoor");
        TimeManager.Instance.NextDay();

    }
}
