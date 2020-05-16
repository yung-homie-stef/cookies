using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : Interactable
{
    public GameObject exitMask;
    public GameObject player;
    public Text noticeText;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void Interact()
    {
        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Game")
                    {
                        exitMask.SetActive(true);
                        _inventory.isSlotFull[i] = false;
                        Destroy(_inventory.playerInventoryItems[i]);
                        Inventory.instance.RemoveItem(Inventory.instance.items[i]);
                        _notice.ChangeText("NEAT");
                        break;
                    }
                }
            }
        }
    }
}
