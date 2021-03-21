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

    private bool VHSEffectOn = true;

    private Settings_Manager settingsManager;

    static Settings globalSettingsCanvas = null;

    void Awake()
    {
        if (globalSettingsCanvas != null)
        {
            Destroy(this.gameObject);
        }

        globalSettingsCanvas = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        settingsManager = Settings_Manager.GetSettingsManager();

        // TODO: load settings manager from file
        InitPostVHSScript();
    }

    public void OpenSettings(GameObject lastMenu)
    {
        _lastMenu = lastMenu;
    }

    public void DisableOrEnableVHSEffect()
    {
        VHSEffectOn = !VHSEffectOn;

        if (VHSEffectOn)
        {
            _postVHSScript.enabled = true;
            settingsManager.VHSEffectOn = true;
            vhsEnabled.text = "ENABLED";
        }
        else
        {
            _postVHSScript.enabled = false;
            settingsManager.VHSEffectOn = false;
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
        settingsManager.filmGrainAmount = grain;
    }

    public void SetBleed(float bleed)
    {
        _postVHSScript.bleedAmount = bleed;
        settingsManager.bleedAmount = bleed;
    }

    public void SetGamma(float gamma)
    {
        _postVHSScript.gammaCorection = gamma;
        settingsManager.gammaCorection = gamma;
    }

    public void SetNoise(float noise)
    {
        _postVHSScript.tapeNoiseTH = noise;
        settingsManager.tapeNoiseTH = noise;
    }


    public void EnableTapeNoise()
    {
        if (_postVHSScript.tapeNoiseOn == false)
        {
            _postVHSScript.tapeNoiseOn = true;
            settingsManager.tapeNoiseOn = true;
            trackingEnabled.text = "ENABLED";
        }
        else if (_postVHSScript.tapeNoiseOn == true)
        {
            _postVHSScript.tapeNoiseOn = false;
            settingsManager.tapeNoiseOn = false;
            trackingEnabled.text = "DISABLED";
        }
    }


    public void CloseSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
        _lastMenu.SetActive(true);
        logo.SetActive(true);
    }

    public static Settings GetSettingsCanvas()
    {
        return globalSettingsCanvas;
    }

    public void InitPostVHSScript()
    {
        _postVHSScript.bleedAmount = settingsManager.bleedAmount;
        _postVHSScript.filmGrainAmount = settingsManager.filmGrainAmount;
        _postVHSScript.gammaCorection = settingsManager.gammaCorection;
        _postVHSScript.tapeNoiseTH = settingsManager.tapeNoiseTH;

        _postVHSScript.tapeNoiseOn = settingsManager.tapeNoiseOn;
        _postVHSScript.enabled = settingsManager.VHSEffectOn;
    }
}
