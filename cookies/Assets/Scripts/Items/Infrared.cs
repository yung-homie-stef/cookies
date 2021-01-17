using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrared : Action
{
    public GameObject VHSCamera;
    public GameObject norman;
    public GameObject screenDarkener;
    public GameObject footprints;
    public Material playerMat;

    private Material[] _heatMaterials;
    private Renderer _NormanRenderer;
    private Renderer _PlayerRenderer;
    private bool _heatOn = false;
    private HeatVision _heatVision;
    

    // Start is called before the first frame update
    void Start()
    {
        _heatVision = VHSCamera.GetComponent<HeatVision>();
        _NormanRenderer = norman.GetComponent<Renderer>();
        _PlayerRenderer = player.GetComponent<Renderer>();
        _heatMaterials = _NormanRenderer.materials;
    }

    public override void Use(int itemIndex)
    {
        if (_heatOn == false)
        {
            foreach (Material mat in _heatMaterials)
                mat.EnableKeyword("_EMISSION");

            playerMat.EnableKeyword("_EMISSION");
            _heatVision.enabled = true;
            footprints.SetActive(true);
            screenDarkener.SetActive(true);
            _heatOn = true;
        }

        else
        {
            foreach (Material mat in _heatMaterials)
                mat.DisableKeyword("_EMISSION");

            playerMat.DisableKeyword("_EMISSION");
            footprints.SetActive(false);
            _heatVision.enabled = false;
            screenDarkener.SetActive(false);
            _heatOn = false;
        }
    }
}
