using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGhost : MonoBehaviour
{
    public Material ghostMat;
    public float spacing = 0.5f;
    private float counter = 0.0f;
    private bool _applyGhost = false;
    private Texture2D _tempTex = null;

    void Start()
    {
        _tempTex = new Texture2D(1920, 1080);
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= spacing)
        {
            counter = 0.0f;
            _applyGhost = true;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_applyGhost)
        {
            _applyGhost = false;
            RenderTexture.active = source;
            _tempTex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
            _tempTex.Apply();
        }

        ghostMat.SetTexture("_SecTex", _tempTex);
        ghostMat.SetFloat("_Percent", (1 - (counter / (spacing * 3.0f))) * 0.5f);

        Graphics.Blit(source, destination, ghostMat);
    }
}
