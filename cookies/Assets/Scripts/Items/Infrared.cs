using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrared : Action
{
    public GameObject VHSCamera;
    public GameObject norman;
    public GameObject screenDarkener;

    private Material[] _heatMaterials;
    private Renderer _renderer;
    private bool _heatOn = false;
    private HeatVision _heatVision;
    

    // Start is called before the first frame update
    void Start()
    {
        _heatVision = VHSCamera.GetComponent<HeatVision>();
        _renderer = norman.GetComponent<Renderer>();
        _heatMaterials = _renderer.materials;
    }

    public override void Use(int itemIndex)
    {
        if (_heatOn == false)
        {
            foreach (Material mat in _heatMaterials)
                mat.EnableKeyword("_EMISSION");

            _heatVision.enabled = true;
            screenDarkener.SetActive(true);
            _heatOn = true;
        }

        else
        {
            foreach (Material mat in _heatMaterials)
                mat.DisableKeyword("_EMISSION");

            _heatVision.enabled = false;
            screenDarkener.SetActive(false);
            _heatOn = false;
        }
    }
}
