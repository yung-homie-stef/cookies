using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Text vhsEnabled;
    public Text fullscreenEnabled;

    private VHSPostProcessEffect _vhsCameraEffect;
    private GameObject _lastMenu;

    public void OpenSettings(GameObject lastMenu)
    {
        _lastMenu = lastMenu;
    }

    public void DisableOrEnableVHSEffect()
    {
        Game_Manager.globalGameManager.VHSEffectOn = !Game_Manager.globalGameManager.VHSEffectOn;

        if (Game_Manager.globalGameManager.VHSEffectOn)
        {
            vhsEnabled.text = "ENABLED";
        }
        else
            vhsEnabled.text = "DISABLED";
    }

    public void DisableOrEnableFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;

        if (isFull)
            fullscreenEnabled.text = "ENABLED";
        else
            fullscreenEnabled.text = "DISABLED";
    }

    public void SetVolume(float vol)
    {
        mixer.SetFloat("mixerVolume", vol);
        Debug.Log(vol);
    }

    public void CloseSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
        _lastMenu.SetActive(true);
    }
}
