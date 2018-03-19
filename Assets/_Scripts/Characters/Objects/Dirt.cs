using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Interactable {
	public GameObject plant;
	public override void Interact(){
		plant.SetActive (true);
	}
}
