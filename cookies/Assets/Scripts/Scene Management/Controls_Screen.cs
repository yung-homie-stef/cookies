using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Screen : MonoBehaviour
{
    private GameObject _lastMenu;

    public void OpenControls(GameObject lastMenu)
    {
        _lastMenu = lastMenu;
    }

    public void CloseControls()
    {
        Game_Manager.globalGameManager.controlsScreen.SetActive(false);
        _lastMenu.SetActive(true);
    }
}
