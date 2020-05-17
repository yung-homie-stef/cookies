using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Camera playerCamera;
    public RaycastHit playerRaycastHit;
    public bool isRoided;
    public GameObject fistHitbox;
    public Image cursorImage;
    public Sprite interactSprite;
    public Sprite originalHUDDot;
    public GameObject inventoryUI;

    private Movement playerMovement;
    private Inventory_UI _inventoryUIScript;
    private Inventory _inventory;
    private Animator _animator;
    private Item lastUsedItem;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        _inventoryUIScript = GetComponent<Inventory_UI>();
        _inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
        isRoided = false;
        DontDestroyOnLoad(gameObject);
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
            if (isRoided)
                Punch();
        }

        if (Input.GetButtonDown("Use"))
        {
            Use();
        }


    }

    private void FixedUpdate()
    {
        Ray _ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out playerRaycastHit, 1))
        {
            if (playerRaycastHit.transform.tag.Equals("Interactable"))
            {
                cursorImage.sprite = interactSprite;
            }
        }
        else
            cursorImage.sprite = originalHUDDot;
    }

    public void DisableMovement()
    {
        playerMovement.playerMovementEnabled = false;
    }

    public void EnableMovement()
    {
        playerMovement.playerMovementEnabled = true;
    }

    void Interact()
    {
        Ray _ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out playerRaycastHit, 1))
        {
            if (_inventory.isWeaponEquipped == false)
            {
                if (playerRaycastHit.transform.tag.Equals("Interactable"))
                {
                    if (playerRaycastHit.transform.GetComponent<Interactable>())
                    {
                        playerRaycastHit.transform.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
    }

    public void OpenInteractInventory(Interactable target)
    {
        _inventory.interactionTarget = target;
        _inventoryUIScript.EnableUI();
    }

    public void CloseInteractInventory()
    {
        _inventoryUIScript.DisableUI();
    }

    void Use()
    {
        //if (_inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<Action>())
        //{
        //    _inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<Action>().Use();
        //}
    }

    public void Drop(int i)
    {
        if (_inventory.isWeaponEquipped == false)
        {
                _inventory.playerInventoryItems[i].GetComponent<AcquirableInteractable>().Drop();
            
        }
    }

    public void RemoveUsedItem()
    {
        _inventory.RemoveItem(lastUsedItem);
    }



    void Punch()
    {
        _animator.Play("punch");
    }

    public void ActivateFistHitbox(int condition)
    {
        if (condition == 1)
        {
            fistHitbox.SetActive(true); // yo why the fuck cant animation events take in bools fuck is this garbage...
        }

        if (condition == 2)
        {
            fistHitbox.SetActive(false);
        }

    }

    public void ActivateMeleeHitbox(int condition)
    {
        if (_inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<MeleeWeapon>())
        {
            _inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<MeleeWeapon>().EnableMeleeHitbox(condition);
        }
    }
}
