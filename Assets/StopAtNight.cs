using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAtNight : MonoBehaviour {

    public bool mustPlay;
    public bool isPlaying;
    FmodEventEmitter emitter;
	// Use this for initialization
	private void Start () 
    {
        emitter = GetComponent<FmodEventEmitter>();
        isPlaying = !Play();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if(Play() != isPlaying)
        {
            SoundManager.Instance.PlaySound(emitter, isPlaying);
            isPlaying = !isPlaying;
        }

	}

    private bool Play()
    {
        Clock clock = TimeManager.Instance.clock;
        if (clock.hour > 19 || clock.hour < 7)
        {
            return false;
        }

        return true;
    }
}
