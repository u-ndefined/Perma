using System.Collections;
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
        


        float temperature = time.clock.hour / 23f * 20f - 20;
        float lightIntensity = time.clock.hour / 23f * 0.3f + 0.5f;


        ColorGradingModel.Settings colorGrading = postProcessing.colorGrading.settings;
        colorGrading.basic.temperature = temperature;
        sunLight.intensity = lightIntensity;
    }
}
