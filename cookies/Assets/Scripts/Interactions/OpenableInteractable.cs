using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenableInteractable : Interactable
{
    public GameObject[] housedContents;
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

            // set all objects that were hid in said container (if it's a cabinet or box etc.)
            // to active only after it is opened, this is to stop players from picking up objects
            // before opening their respective containers
            for (int i = 0; i < housedContents.Length; i++)
            {
                housedContents[i].SetActive(true);
                housedContents[i].GetComponent<BoxCollider>().enabled = true;
            }
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
            for (int i = 0; i < _inventory.UISlots.Length; i++)
            {
                _tags = _inventory.inventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == requiredKey) // if they do, destroy the key and unlock the door
                    {
                        isLocked = false;
                        _inventory.isFull[i] = false;
                        Destroy(_inventory.inventoryItems[i]);
                        break;
                    }
                    else
                    {
                        if (isLocked)
                        _notice.ChangeText(newText);
                    }
                }
            }
        }
        else // otherwise open it normally
        {
            isOpened = !isOpened;
            EnactOpening();
        }
    }
}
