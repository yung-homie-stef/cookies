using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessMaterial : MonoBehaviour
{
    public Material mat = null;
    public float oscillatingSpeed = 0.1f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat)
        {
            mat.SetFloat("_HighThreshold", Mathf.PingPong((Time.time * oscillatingSpeed), 0.5f));
            Graphics.Blit(source, destination, mat);
        }
        else
        {
            Debug.Log("no material assigned");
        }
    }
}
