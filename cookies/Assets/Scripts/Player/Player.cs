using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public RaycastHit hit;
    public bool roided;

    private Movement playerMovement;
    private Inventory _inventory;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        _inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
        roided = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }

        if (Input.GetButton("Fire2"))
        {
            if (roided)
                Punch();
        }

        if (Input.GetButtonDown("Use"))
        {
            Use();
        }

        if (Input.GetButtonDown("Drop"))
        {
            Drop();
        }

    }

    public void DisableMovement()
    {
        playerMovement.movementEnabled = false;
    }

    void Interact()
    {
        Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, 1))
        {
            if (_inventory.weaponEquipped == false)
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

    void Use()
    {
        if (_inventory.inventoryItems[Inventory.currentSlot].GetComponent<Action>())
        {
            _inventory.inventoryItems[Inventory.currentSlot].GetComponent<Action>().Use();
        }
    }

    void Drop()
    {
        if (_inventory.weaponEquipped == false)
        {
            if (_inventory.inventoryItems[Inventory.currentSlot].GetComponent<AcquirableInteractable>() && _inventory.inventoryItems[Inventory.currentSlot])
            {
                _inventory.inventoryItems[Inventory.currentSlot].GetComponent<AcquirableInteractable>().Drop();
            }
        }
    }

    void Punch()
    {
        _animator.Play("punch");
    }
}
