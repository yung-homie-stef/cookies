using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb_Table : Interactable
{
    public GameObject pipeBomb;
    public GameObject player;
    public Text noticeText;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;
    private bool hasCrafted;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        hasCrafted = false;
    }

    public override void Interact()
    {
        if (!hasCrafted)
        {
            for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
            {
                if (_inventory.playerInventoryItems[i] != null)
                {
                    _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                    for (int j = 0; j < _tags.tags.Length; j++)
                    {
                        if (_tags.tags[j] == "Pipe")
                        {
                            _inventory.isSlotFull[i] = false;
                            Destroy(_inventory.playerInventoryItems[i]);
                            break;
                        }
                    }
                }
            }
        }
        if (!hasCrafted)
        {
            _notice.ChangeText("LEAD PIPE REQUIRED");
        }
    }
}
