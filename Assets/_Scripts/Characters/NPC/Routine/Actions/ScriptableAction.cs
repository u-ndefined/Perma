using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableAction : ScriptableObject {
    public virtual void Act(Transform actor)
    {
        Debug.Log("Act");
    }
}
