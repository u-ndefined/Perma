using System.Collections;
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
        Fade2.Instance.FadeOut(true, 2);
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        Fade2.Instance.onFadeEndEvent += ChangeScene;

    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Fade2.Instance.FadeOut(true, 2);
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        Fade2.Instance.onFadeEndEvent += RealQuit;
    }

    private void RealQuit()
    {
        Application.Quit();
    }

    public void ToggleSettings()
    {
        SoundManager.Instance.PlaySound("UI/ClickMenu");
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
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        if (onCredits)
        {
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
