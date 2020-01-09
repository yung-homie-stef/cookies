using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement PlayerMovement;
    public Camera mainCamera;
    public RaycastHit hit;
    //public Inventory PlayerInventory; todo

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update controls
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    public void DisableMovement()
    {
        PlayerMovement.movementEnabled = false;
    }

    void Interact()
    {
        Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, 1))
        {
            if (hit.transform.tag.Equals("Interactable"))
            {
                if (hit.transform.GetComponent<Interactable>())
                {
                    hit.transform.GetComponent<Interactable>().Interact();
                }
            }
        }
    }
}
