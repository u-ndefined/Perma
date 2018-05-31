using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour {
    TimeManager time;
    TextMeshProUGUI textMesh;
    public GameObject disk;
	// Use this for initialization
	void Start () 
    {
        time = TimeManager.Instance;
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        int demie = Mathf.FloorToInt(time.clock.minute / 30) * 30;
        string hour = string.Format("{0:00} : {1:00}", time.clock.hour, demie);
        textMesh.text = hour;

        disk.transform.rotation = Quaternion.Euler(new Vector3(0,0,time.TimeNormalised() * 360));
	}
}
