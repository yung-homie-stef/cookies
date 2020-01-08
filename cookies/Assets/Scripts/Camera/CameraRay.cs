using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public Camera mainCamera;
    public bool isHitting = false;
    public RaycastHit hit;

    private void Update()
    {
        Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, 1))
        {
            if (hit.transform.tag.Equals("Interactable"))
            {
                isHitting = true;
            }
        }
        else
            isHitting = false;
    }


}
