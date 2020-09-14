using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_LOS : MonoBehaviour
{
    public Camera affectedCamera;

    public float nearDistance = 0.0f;
    public float farDistance = 0.0f;

    public bool adjustFarClipPlane;
    public bool adjustNearClipPlane;

    private void OnTriggerEnter(Collider other)
    {
        if (adjustFarClipPlane)
        affectedCamera.farClipPlane = farDistance;

        if (adjustNearClipPlane)
        affectedCamera.nearClipPlane = nearDistance;
    }
}
