using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject gameTitle;
    public GameObject startMenu;
    public GameObject startButton;
    public GameObject logo;

    public static MainMenu globalMainMenuManager = null;

    public void RemoveTitle()
    {
        gameTitle.SetActive(false);
        startMenu.SetActive(true);
        startButton.SetActive(false);
    }

    private void Awake()
    {
        
    }

    public void StartGame()
    {
        // load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Audio_Manager.globalAudioManager.musicSoundArray[1].source.Stop();
        Audio_Manager.globalAudioManager.PlaySound("tape", Audio_Manager.globalAudioManager.intangibleSoundArray);
        Cursor.visible = false;

        logo.SetActive(false);
        startMenu.SetActive(false);


        if (globalMainMenuManager)
        {
            Destroy(gameObject);
            return;
        }

        globalMainMenuManager = this;
    }


    public void ExitGame()
    {
        // quit the game
        Application.Quit();
    }

    public void GoBack()
    {
        // go back to start menu
        Game_Manager.globalGameManager.threadScreen.SetActive(false);
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
        startMenu.SetActive(true);
        Game_Manager.globalGameManager.tapes.SetActive(false);
        logo.SetActive(true);
    }

    public void CheckThreads()
    {
        // go to threads menu
        startMenu.SetActive(false);
        Game_Manager.globalGameManager.threadScreen.SetActive(true);
        Game_Manager.globalGameManager.tapes.SetActive(true);
        logo.SetActive(false);
    }

    public void OpenSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.GetComponent<Settings>().OpenSettings(startMenu);
        Game_Manager.globalGameManager.settingsScreen.SetActive(true);
        startMenu.SetActive(false);
        logo.SetActive(false);
    }

    public void OpenControls()
    {
        Game_Manager.globalGameManager.controlsScreen.GetComponent<Controls_Screen>().OpenControls(startMenu);
        Game_Manager.globalGameManager.controlsScreen.SetActive(true);
        startMenu.SetActive(false);
        logo.SetActive(false);
    }

    public void ResetFirstScreen()
    {
        logo.SetActive(true);
        gameTitle.SetActive(true);
        startButton.SetActive(true);
    }
}
