﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DayNightRendering : MonoBehaviour {
    private TimeManager time;
    public Light sunLight;
    private PostProcessingProfile postProcessing;

	// Use this for initialization
	void Start () 
    {
        time = TimeManager.Instance;
        postProcessing = GetComponent<PostProcessingBehaviour>().profile;
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdatePostProcessing();
	}

    private void UpdatePostProcessing()
    {
        


        //float temperature = time.clock.hour / 23f * 20f - 20;
		float lightIntensity = -0.0027777777777778f * time.clock.hour * time.clock.hour + 0.066666666666667f * time.clock.hour + 0.6f; //http://fsincere.free.fr/equation_parabole/equation_parabole.php
        //float lightIntensity = -0.0020833333333333f * time.clock.hour * time.clock.hour + 0.05f * time.clock.hour + 0.5f;

        //ColorGradingModel.Settings colorGrading = postProcessing.colorGrading.settings;
        //colorGrading.basic.temperature = temperature;
        sunLight.intensity = lightIntensity;
    }
}
