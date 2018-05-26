using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
	public Transform target;

	public Vector3 offset;
    public float smoothSpeed = 0.125f;



    [Header("OSEF")]
	public float pitch = 2f;
	private float currentZoom = 10f;
	public float zoomSpeed = 4f;
	public float minZoom = 5f;
	public float maxZoom = 15f;

	public float yawSpeed = 100f;
	public float currentYaw = 0f;
	// Use this for initialization
	void Start () {
	}

	void Update(){
		//currentZoom -= Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		//currentZoom = Mathf.Clamp (currentZoom, minZoom, maxZoom);
        if (Input.GetButton("Modifier1"))
        {
            currentYaw -= Input.GetAxis("Mouse ScrollWheel") * yawSpeed * Time.deltaTime;
        }

		//currentYaw -= Input.GetAxis ("Horizontal") * yawSpeed * Time.deltaTime; 
	}
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
		//transform.LookAt (target.position + Vector3.up * pitch);
		//transform.RotateAround (target.position, Vector3.up, currentYaw);
	}


}
