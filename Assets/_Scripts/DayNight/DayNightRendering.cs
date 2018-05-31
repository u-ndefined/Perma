using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DayNightRendering : MonoBehaviour {
    private TimeManager time;
    public Light sceneLight;
    public Color dayLight;
    public Color nightLight;
    private PostProcessingBehaviour postProcessing;
    public PostProcessingProfile day;
    public PostProcessingProfile night;
    private ColorGradingModel.Settings settings;
    private Vector3[] parameters;
    private Vector3 dayR, dayG, dayB, nightR, nightG, nightB;

	// Use this for initialization
	void Start () 
    {
        time = TimeManager.Instance;
        postProcessing = GetComponent<PostProcessingBehaviour>();
        //ColorGradingModel.Settings colorGrading = ;
        //colorGrading.basic.temperature = 30;
        //postProcessing.profile = night;
        settings = new ColorGradingModel.Settings();
        settings = postProcessing.profile.colorGrading.settings;
        parameters = new Vector3[6];
        Debug.Log(night.colorGrading.settings.basic.temperature +" "+ day.colorGrading.settings.basic.temperature);
        parameters[0] = Solve(night.colorGrading.settings.basic.contrast, day.colorGrading.settings.basic.contrast);
        parameters[1] = Solve(night.colorGrading.settings.basic.hueShift, day.colorGrading.settings.basic.hueShift);
        parameters[2] = Solve(night.colorGrading.settings.basic.postExposure, day.colorGrading.settings.basic.postExposure);
        parameters[3] = Solve(night.colorGrading.settings.basic.saturation, day.colorGrading.settings.basic.saturation);
        parameters[4] = Solve(night.colorGrading.settings.basic.temperature, day.colorGrading.settings.basic.temperature);
        parameters[5] = Solve(night.colorGrading.settings.basic.tint, day.colorGrading.settings.basic.tint);
        dayR = day.colorGrading.settings.channelMixer.red;
        dayG = day.colorGrading.settings.channelMixer.green;
        dayB = day.colorGrading.settings.channelMixer.blue;
        nightR = night.colorGrading.settings.channelMixer.red;
        nightG = night.colorGrading.settings.channelMixer.green;
        nightB = night.colorGrading.settings.channelMixer.blue;

            
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdatePostProcessing();
	}

    private void UpdatePostProcessing()
    {
        float h = time.TimeNormalised() * 24f;
        float hn = time.TimeNormalised();

        settings.basic.contrast = GetValue(parameters[0], h);
        settings.basic.hueShift = GetValue(parameters[1], h);
        settings.basic.postExposure = GetValue(parameters[2], h);
        settings.basic.saturation = GetValue(parameters[3], h);
        settings.basic.temperature = GetValue(parameters[4], h);
        settings.basic.tint = GetValue(parameters[5], h);

        settings.channelMixer.red = GetVector(nightR, dayR, hn);
        settings.channelMixer.green = GetVector(nightG, dayG, hn);
        settings.channelMixer.blue = GetVector(nightB, dayB, hn);

        postProcessing.profile.colorGrading.settings = settings;

        sceneLight.color = GetColor(nightLight, dayLight, hn);
    }

    private Vector3 Solve(float n , float d )
    {
        Vector3 result = Vector3.zero;
        result.x = -(d - n) / 144f;
        result.y = ((d - n) / 12f) - (result.x * 12f);
        result.z = n;
        return result;
    }

    private float GetValue(Vector3 p, float h)
    {
        return p.x * h * h + p.y * h + p.z;
	}

    private Vector3 GetVector(Vector3 n, Vector3 d, float h)
    {
        float f = h < 0.5f ? h * 2f : 2f - (h * 2f);
        return Vector3.Lerp(n, d, f);
    }

    private Color GetColor(Color n, Color d, float h)
    {
        float f = h < 0.5f ? h * 2f : 2f - (h * 2f);
        return Color.Lerp(n, d, f);
    }
}
