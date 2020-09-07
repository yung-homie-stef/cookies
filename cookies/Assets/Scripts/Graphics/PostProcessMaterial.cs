using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessMaterial : MonoBehaviour
{
    public Material mat = null;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat)
        {
            Graphics.Blit(source, destination, mat);
        }
        else
        {
            Debug.Log("no material assigned");
        }
    }
}
