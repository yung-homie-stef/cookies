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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // make game title disappear and open up the start menu
            gameTitle.SetActive(false);
            startMenu.SetActive(true);
        }
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
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void CheckThreads()
    {
        // go to threads menu
        startMenu.SetActive(false);
        threadMenu.SetActive(true);
    }
}
