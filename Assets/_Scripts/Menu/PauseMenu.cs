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
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        timeManager.Play();
    }

	public void Pause()
	{
        pauseMenuUI.SetActive(true);
        timeManager.Pause();
	}

    public void SaveAndQuit()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ToggleSettings()
    {
        onSettings = !onSettings;
        settings.SetActive(onSettings);
        pauseMenu.SetActive(!onSettings);
    }
}
