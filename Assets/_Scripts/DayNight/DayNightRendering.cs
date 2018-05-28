using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DayNightRendering : MonoBehaviour {
    private TimeManager time;
    public Light sunLight;
    public PostProcessingBehaviour postProcessing;
    public PostProcessingProfile day;

	// Use this for initialization
	void Start () 
    {
        time = TimeManager.Instance;
        postProcessing = GetComponent<PostProcessingBehaviour>();
        //ColorGradingModel.Settings colorGrading = ;
        //colorGrading.basic.temperature = 30;

	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdatePostProcessing();
	}

    private void UpdatePostProcessing()
    {

        float h = time.clock.hour;
        float temperature = -0.34722222222222f * h * h + 8.3333333333333f * h - 50f;
        float tint = 0.13888888888889f * h * h - 3.3333333333333f * h + 20; 
		float lightIntensity = -0.0027777777777778f * h * h + 0.066666666666667f * h + 0.6f; //http://fsincere.free.fr/equation_parabole/equation_parabole.php
        //float lightIntensity = -0.0020833333333333f * time.clock.hour * time.clock.hour + 0.05f * time.clock.hour + 0.5f;

        //ColorGradingModel.Settings colorGrading = postProcessing.colorGrading.settings;
        //colorGrading.basic.temperature = 33;
        sunLight.intensity = lightIntensity;


        ColorGradingModel.Settings ok = new ColorGradingModel.Settings();
        ok = day.colorGrading.settings;
        ok.basic.temperature = temperature;
        ok.basic.tint = tint;
        postProcessing.profile.colorGrading.settings = ok;
    }
}
