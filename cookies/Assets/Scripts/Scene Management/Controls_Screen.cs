using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls_Screen : MonoBehaviour
{
    private GameObject _lastMenu;
    public GameObject logo;

    public void OpenControls(GameObject lastMenu)
    {
        _lastMenu = lastMenu;
    }

    public void CloseControls()
    {
        Game_Manager.globalGameManager.controlsScreen.SetActive(false);
        _lastMenu.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Start")
        {
            logo.SetActive(true);
        }
    }
}
