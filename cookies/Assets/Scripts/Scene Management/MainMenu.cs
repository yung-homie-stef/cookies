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
    public GameObject threadMenu;
    public GameObject startButton;
    public GameObject tapes;
    public GameObject logo;

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
        Audio_Manager.globalAudioManager.musicSoundArray[1].source.Stop();
        Audio_Manager.globalAudioManager.PlaySound("tape", Audio_Manager.globalAudioManager.intangibleSoundArray);
        Cursor.visible = false;
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
        tapes.SetActive(false);
        logo.SetActive(true);
    }

    public void CheckThreads()
    {
        // go to threads menu
        startMenu.SetActive(false);
        threadMenu.SetActive(true);
        tapes.SetActive(true);
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
}
