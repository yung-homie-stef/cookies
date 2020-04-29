using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject vhsCamera;
    public AudioMixer mixer;
    public Text vhsEnabled;
    public Text fullscreenEnabled;

    private VHSPostProcessEffect _vhsCameraEffect;
    private GameObject _lastMenu;

    // Start is called before the first frame update
    void Start()
    {
        _vhsCameraEffect = vhsCamera.GetComponent<VHSPostProcessEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void DisableOrEnableFullscreen()
    {

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
