using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueBox : MonoBehaviour {
	public TextMeshProUGUI textMeshPro;
	public void Display(){
        gameObject.SetActive(true);
        /*
		foreach (Transform child in transform) {
			child.gameObject.SetActive (true);
		}
		*/

	}
	public void Hide(){
        gameObject.SetActive(false);
        /*
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
		*/
	}

}
