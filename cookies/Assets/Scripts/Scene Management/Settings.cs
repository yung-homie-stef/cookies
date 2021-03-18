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

    public postVHSPro _postVHSScript;

    private VHSPostProcessEffect _vhsCameraEffect;
    private GameObject _lastMenu;

    private bool tracking = true;
    private bool isOn = true;

    public void OpenSettings(GameObject lastMenu)
    {
        _lastMenu = lastMenu;
    }

    public void DisableOrEnableVHSEffect()
    {
        isOn = !isOn;

        if (isOn)
        {
            _postVHSScript.enabled = true;
            vhsEnabled.text = "ENABLED";
        }
        else
        {
            _postVHSScript.enabled = false;
            vhsEnabled.text = "DISABLED";
        }

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
       postVHSPro.filmGrainAmount = grain;
    }

    public void SetBleed(float bleed)
    {
        postVHSPro.bleedAmount = bleed;
    }

    public void SetGamma(float gamma)
    {
        postVHSPro.gammaCorection = gamma;
    }

    public void SetNoise(float noise)
    {
        postVHSPro.tapeNoiseTH = noise;
    }


    public void EnableTapeNoise()
    {
        if (postVHSPro.tapeNoiseOn == false)
        {
            postVHSPro.tapeNoiseOn = true;
            trackingEnabled.text = "ENABLED";
        }
        else if (postVHSPro.tapeNoiseOn == true)
        {
            postVHSPro.tapeNoiseOn = false;
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
