using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	private Camera cam;
    public Vector3 offset;
    public Transform character;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
        //character = GetComponentInParent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(2 * transform.position - cam.transform.position);
        transform.position = character.position + offset;
	}

    public void Tourne()
    {
        cam = Camera.main;
        transform.LookAt(2 * transform.position - cam.transform.position);
        transform.position = character.position + offset;
    }

}
