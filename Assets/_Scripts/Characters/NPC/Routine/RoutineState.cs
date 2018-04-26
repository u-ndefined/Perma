using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoutineState
{
    public Date date;
    public ScriptableDialogues mainDialogue;
    public ScriptableDialogues[] sideDialogues;
    public ScriptableAction[] actions;

}
