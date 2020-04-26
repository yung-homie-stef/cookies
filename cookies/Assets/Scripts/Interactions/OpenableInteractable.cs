using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class OpenableInteractable : Interactable
{
    public GameObject player;
    public string requiredKey;
    public string newText;
    public bool isLocked;
    public Text noticeText;

    private bool isOpened;
    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        _animator = GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public void EnactOpening()
    {
        if (isOpened == true)
        {
            // play the opening animation
            _animator.SetBool("is_opened", true);
            
        }

        else if (isOpened == false)
        {
            _animator.SetBool("is_opened", false); ;
        }
    }

    public override void Interact()
    {
        if (isLocked) // if the door is locked check to see if they have the right key
        {
            for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
            {
                if (_inventory.playerInventoryItems[i] != null)
                {
                    _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                    for (int j = 0; j < _tags.tags.Length; j++)
                    {
                        if (_tags.tags[j] == requiredKey) // if they do, destroy the key and unlock the door
                        {
                            isLocked = false;
                            _inventory.isSlotFull[i] = false;
                            Destroy(_inventory.playerInventoryItems[i]);
                            break;
                        }
                    }
                }
            }
            if (isLocked != false)
            {
                _notice.ChangeText(newText);
            }
        }
        else // otherwise open it normally
        {
            isOpened = !isOpened;
            EnactOpening();
        }
    }

    public void PlayDoorSound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(Door_Sound_Effects.globalDoorSounds.doorSFX[clip]);
    }
}
