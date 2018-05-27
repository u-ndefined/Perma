﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationMenu : MonoBehaviour 
{
    public GameObject settings, start, credits;
    private bool onSettings = false;
    private bool onCredits = false;

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleSettings()
    {
        if(onSettings)
        {
            settings.SetActive(false);
            start.SetActive(true);
        }
        else
        {
            settings.SetActive(true);
            start.SetActive(false);
        }

        onSettings = !onSettings;
    }

    public void ToggleCredits()
    {
        if (onCredits)
        {
            Debug.Log("la");
            credits.SetActive(false);
            start.SetActive(true);
        }
        else
        {
            credits.SetActive(true);
            start.SetActive(false);
        }

        onCredits = !onCredits;
    }


}