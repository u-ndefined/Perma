using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Action: go to object", menuName = "Action/GoToObject")]
public class GoToObject : ScriptableAction {

    public string targetName;

    public override void Act(Transform actor)
    {
        Debug.Log("hello");
        Transform target = GameObject.Find(targetName).GetComponent<Transform>();


        if(target != null)
        {
            Debug.Log(target.name);
            Debug.Log(target.position);
            //actor.GetComponent<Motor>().FollowTarget(target);
        }

    }
}
