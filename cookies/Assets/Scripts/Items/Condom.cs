using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condom : Action
{
    public GameObject inventoryUI;
    public Player playerScript;

    private Movement _movement;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            playerScript.playerHealth += 3;

            Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
            Destroy(_inventory.playerInventoryItems[itemIndex]);
            Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

            inventoryUI.GetComponent<Inventory_UI>().DisableUI();
        }
    }
}
