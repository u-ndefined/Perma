using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    
    private GameObject pauseMenuUI;
    private TimeManager timeManager;
    private bool onSettings = false;
    public GameObject pauseMenu;
    public GameObject settings;

	private void Start()
	{
        pauseMenuUI = transform.GetChild(0).gameObject;
        timeManager = TimeManager.Instance;
	}

	private void Update()
	{
        if(Input.GetButtonDown("Pause"))
        {
            if(timeManager.gameIsPaused)
            {
                if (onSettings) ToggleSettings();
                else Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        pauseMenuUI.SetActive(false);
        timeManager.Play();
    }

	public void Pause()
	{
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        pauseMenuUI.SetActive(true);
        timeManager.Pause();
	}

    public void SaveAndQuit()
    {
        Fade.Instance.FadeOut(true, 2);
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        Fade.Instance.onFadeEndEvent += Restart;
        //SoundManager.Instance.PlaySound("UI/ClickMenu");
        //Resume();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void Restart()
    {
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program 
        Application.Quit(); //kill current process
    }


    public void ToggleSettings()
    {
        SoundManager.Instance.PlaySound("UI/ClickMenu");
        onSettings = !onSettings;
        settings.SetActive(onSettings);
        pauseMenu.SetActive(!onSettings);
    }
}
