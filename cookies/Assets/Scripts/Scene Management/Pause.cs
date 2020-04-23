using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
               
            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
