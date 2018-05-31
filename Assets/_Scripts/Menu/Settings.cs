using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {

    private Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    private float previousVolume;
    private bool muted = false;
    private Slider slider;

	private void Start()
	{
        SetupResolutionDropdown();
        slider = GetComponentInChildren<Slider>();
	}

	public void SetVolume(float volume)
    {
        
    }

    public void Mute()
    {
        if(muted)
        {
            SetVolume(previousVolume);
            slider.value = previousVolume;
            muted = false;
        }
        else
        {
            previousVolume = 10;//get volume
            SetVolume(0);
            slider.value = 0;
            muted = true;
        }

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
