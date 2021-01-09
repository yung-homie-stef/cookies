using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public AudioMixer sfxMixer;
    public AudioMixer musicMixer;
    public Text vhsEnabled;
    public Text fullscreenEnabled;
    public Text trackingEnabled;
    public GameObject logo;

    public float grain;
    public float bleed; // settings for video
    public float gamma;
    public float phosphor;

    private VHSPostProcessEffect _vhsCameraEffect;
    private GameObject _lastMenu;
    private postVHSPro _postVHSScript;
    private bool tracking = true;

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

    public void SetSFXVolume(float vol)
    {
        sfxMixer.SetFloat("mixerVolume", vol);
        Debug.Log(vol);
    }

    public void SetMusicVolume(float vol)
    {
        musicMixer.SetFloat("mixerVolume", vol);
    }

    public void SetGrain(float grain)
    {
       postVHSPro.globalVHSFX.filmGrainAmount = grain;
    }

    public void SetBleed(float bleed)
    {
        postVHSPro.globalVHSFX.bleedAmount = bleed;
    }

    public void SetGamma(float gamma)
    {
       postVHSPro.globalVHSFX.gammaCorection = gamma;
    }

    public void SetVerticalResolution()
    {
        tracking = !tracking;

        if (tracking)
        {
            postVHSPro.globalVHSFX.noiseLinesMode = 1;
            postVHSPro.globalVHSFX.noiseLinesNum = 1024;
            trackingEnabled.text = "ENABLED";
        }
        else
        {
           postVHSPro.globalVHSFX.noiseLinesMode = 0;
           trackingEnabled.text = "DISABLED";
        }
    }


    public void CloseSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
        _lastMenu.SetActive(true);
        logo.SetActive(true);
    }
}
