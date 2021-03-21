using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Manager : MonoBehaviour
{
    static Settings_Manager globalSettingsManager = null;

    public bool tapeNoiseOn = true;
    public bool VHSEffectOn = true;

    public float bleedAmount = 3.5f;
    public float filmGrainAmount = 0.018f;
    public float gammaCorection = 1f;
    public float tapeNoiseTH = 0.64f;

    private void Awake()
    {
        if (globalSettingsManager != null)
        {
            Destroy(this.gameObject);
        }

        globalSettingsManager = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static Settings_Manager GetSettingsManager()
    {
        return globalSettingsManager;
    }
}
