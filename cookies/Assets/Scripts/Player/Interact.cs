using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera mainCamera;

    private CameraRay _cameraRay;
    private Interactable _interactable;

    // Start is called before the first frame update
    void Start()
    {
        _cameraRay = mainCamera.GetComponent<CameraRay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_cameraRay.isHitting)
            {

            }
        }
    }
}
