using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject options;

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void HideOptions()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

	public void Quit()
	{
        Application.Quit();
	}
}
