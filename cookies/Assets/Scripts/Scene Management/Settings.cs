using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject vhsCamera;
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
}
