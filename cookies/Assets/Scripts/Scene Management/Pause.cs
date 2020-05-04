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
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        HUDDot.SetActive(false);
        pauseCanvas.SetActive(true);
        _videoPlayer.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.GetComponent<Settings>().OpenSettings(pauseCanvas);
        Game_Manager.globalGameManager.settingsScreen.SetActive(true);
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
}
