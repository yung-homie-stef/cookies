using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject vhsCamera;
    public AudioMixer mixer;

    private VHSPostProcessEffect _vhsCameraEffect; 

    // Start is called before the first frame update
    void Start()
    {
        _vhsCameraEffect = vhsCamera.GetComponent<VHSPostProcessEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableOrEnableVHSEffect()
    {
        _vhsCameraEffect.enabled = !_vhsCameraEffect.enabled;
    }

    public void SetVolume(float vol)
    {
        mixer.SetFloat("mixerVolume", vol);
        Debug.Log(vol);
    }

    public void CloseSettings()
    {
        Game_Manager.globalGameManager.settingsScreen.SetActive(false);
    }
}
