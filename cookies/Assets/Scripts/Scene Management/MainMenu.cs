using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject gameTitle;
    public GameObject startMenu;
    public GameObject threadMenu;
    public GameObject settingsMenu;
    public GameObject startButton;

    public Text[] threadTitleTexts = new Text[12];

    public void RemoveTitle()
    {
        gameTitle.SetActive(false);
        startMenu.SetActive(true);
        startButton.SetActive(false);
    }

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
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
        startMenu.SetActive(true);
        
    }

    public void CheckThreads()
    {
        // go to threads menu
        startMenu.SetActive(false);
        threadMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.GetComponent<Settings>().OpenSettings(startMenu);
        Game_Manager.globalGameManager.settingsScreen.SetActive(true);
        startMenu.SetActive(false);

    }
}
