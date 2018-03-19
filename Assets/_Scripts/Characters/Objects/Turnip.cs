using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnip : Interactable {

	public override void Interact(){
		gameObject.SetActive (false);
	}
}
