using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Drink : Action
{
    public GameObject player;
    public Text noticeText;
    public string newText;

    private Movement _movement;
    private Inventory _inventory;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _movement = player.GetComponent<Movement>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void Use()
    {
        _movement.speed *= 2; // become faster
        _inventory.isFull[_inventory.GetCurrentSlot()] = false;
        _notice.ChangeText(newText);
        Destroy(_inventory.inventoryItems[_inventory.GetCurrentSlot()]);
    }

}
