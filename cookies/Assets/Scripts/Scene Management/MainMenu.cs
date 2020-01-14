using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject gameTitle;
    public GameObject startMenu;
    public GameObject threadMenu;
    public GameObject settingsMenu;
    public GameObject firstButton;


    public void StartGame()
    {
        // load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        // quit the game
        Application.Quit();
    }

    public void GoBack()
    {
        // go back to start menu
        threadMenu.SetActive(false);
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void CheckThreads()
    {
        // go to threads menu
        startMenu.SetActive(false);
        threadMenu.SetActive(true);
    }
}
