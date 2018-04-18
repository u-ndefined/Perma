using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New scriptable dialogues", menuName = "Dialogue/ScriptableDialogue")]
[System.Serializable]
public class ScriptableDialogues : ScriptableObject {
    public string dialogueName;

    [TextArea(3, 20)]
    public string[] sentences;
}
