using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New action: go to object", menuName = "Action/GoToObject")]
public class GoToObject : ScriptableAction
{
    public Transform target;
}
