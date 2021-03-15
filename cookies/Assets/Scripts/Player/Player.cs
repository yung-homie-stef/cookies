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
    public Item lastUsedItem;
    public Inventory _inventory;

    public int playerHealth;
    public Image deathScreen;
    public Image rewindIcon;
    public Transform respawnPoint;
    public Respawn_Point respawnScript;

    private Movement playerMovement;
    private Inventory_UI _inventoryUIScript;
    
    private Animator _animator;
   

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        _inventoryUIScript = inventoryUI.GetComponent<Inventory_UI>();
        Debug.Log(_inventoryUIScript);
        _inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
        isRoided = false;
       // DontDestroyOnLoad(gameObject);
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
        Debug.Log(target);
        _inventory.interactionTarget = target;
        _inventoryUIScript.EnableUI();
    }

    public void CloseInteractInventory()
    {
        _inventoryUIScript.DisableUI();
    }

    void Use()
    {
    
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

    public void DestroyUsedItem()
    {
        _inventory.DestroyItem(lastUsedItem);
    }


    void Punch()
    {
        _animator.Play("punch");
    }

    public void ActivateFistHitbox(int condition)
    {
        if (isRoided == true)
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

        else if (_inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<Brass_Knuckles>())
        {

            _inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<Brass_Knuckles>().EnableMeleeHitbox(condition);
        }

    }

    public void ActivateMeleeHitbox(int condition)
    {
        if (_inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<MeleeWeapon>())
        {
            _inventory.playerInventoryItems[Inventory.currentSelectedSlot].GetComponent<MeleeWeapon>().EnableMeleeHitbox(condition);
        }
    }

    public void TakeDamage()
    {
        playerHealth--;
        Debug.Log(playerHealth);

        if (playerHealth == 0)
        {
            Audio_Manager.globalAudioManager.PlaySound("rewind", Audio_Manager.globalAudioManager.intangibleSoundArray);
            deathScreen.enabled = true;
            rewindIcon.enabled = true;
            playerMovement.enabled = false;
            StartCoroutine(KillPlayer(2.0f));
        }
    }

    private IEnumerator KillPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = respawnPoint.position;
        playerHealth = 6;
        playerMovement.enabled = true;
        deathScreen.enabled = false;
        rewindIcon.enabled = false;
        respawnScript.ResetBossFight();
    }
}
