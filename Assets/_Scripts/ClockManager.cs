using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour {
    TimeManager time;
    TextMeshProUGUI textMesh; 
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
        string hour = time.clock.hour + " : " + demie;
        textMesh.text = hour;
	}
}
