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
       _postVHSScript.filmGrainAmount = grain;
    }

    public void SetBleed(float bleed)
    {
        _postVHSScript.bleedAmount = bleed;
    }

    public void SetGamma(float gamma)
    {
        _postVHSScript.gammaCorection = gamma;
    }

    public void SetNoise(float noise)
    {
        _postVHSScript.tapeNoiseTH = noise;
    }


    public void EnableTapeNoise()
    {
        if (_postVHSScript.tapeNoiseOn == false)
        {
            _postVHSScript.tapeNoiseOn = true;
            trackingEnabled.text = "ENABLED";
        }
        else if (_postVHSScript.tapeNoiseOn == true)
        {
            _postVHSScript.tapeNoiseOn = false;
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
