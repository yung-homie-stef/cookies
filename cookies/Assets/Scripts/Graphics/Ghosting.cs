using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosting : MonoBehaviour
{
    public RenderTexture mainTex;
    private Texture2D _tempTex;
    public Material ghostMat;

    public float spacing = 0.1f;
    private float counter = 0.0f;

    private void Start()
    {
        _tempTex = new Texture2D(1920, 1080);
    }

    // Update is called once per frame
    void Update()
    {
        ghostMat.SetTexture("_SecTex", _tempTex);

        counter += Time.deltaTime;

        if (counter >= spacing)
        {
            counter = 0;
            RenderTexture.active = mainTex;
            _tempTex.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);
            _tempTex.Apply();
        }
    }
}
