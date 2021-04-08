using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseCanvas;
    public GameObject VHSCamera;
    public GameObject HUDDot;
    public GameObject quitOptions;
    public GameObject mainMenuOptions;

    public GameObject COCKBLOCKER;

    public GameObject inventory_UI;

    private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer = VHSCamera.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                
                Game_Manager.globalGameManager.settingsScreen.SetActive(false);
                Game_Manager.globalGameManager.controlsScreen.SetActive(false);
            }
            else
            {

                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        HUDDot.SetActive(true);
        pauseCanvas.SetActive(false);
        quitOptions.SetActive(false);
        _videoPlayer.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        COCKBLOCKER.SetActive(false);
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        HUDDot.SetActive(false);
        pauseCanvas.SetActive(true);
        inventory_UI.SetActive(false);
        _videoPlayer.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        COCKBLOCKER.SetActive(true);
    }

    public void OpenSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.GetComponent<Settings>().OpenSettings(pauseCanvas);
        Game_Manager.globalGameManager.settingsScreen.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void OpenControls()
    {
        Game_Manager.globalGameManager.controlsScreen.GetComponent<Controls_Screen>().OpenControls(pauseCanvas);
        Game_Manager.globalGameManager.controlsScreen.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void DontQuit()
    {
        quitOptions.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void DontGoToMainMenu()
    {
        mainMenuOptions.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void OpenMainMenuPrompt()
    {
        mainMenuOptions.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        MainMenu.globalMainMenuManager.ResetFirstScreen();
        Audio_Manager.globalAudioManager.PlaySound("intro", Audio_Manager.globalAudioManager.musicSoundArray);
        Audio_Manager.globalAudioManager.musicSoundArray[0].source.Stop();
        SceneManager.LoadScene("Start");
    }

    public void OpenQuitPrompt()
    { 
        quitOptions.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LiterallyFuckingNothing()
    {
        Debug.Log("veiny chungus");
    }
}
