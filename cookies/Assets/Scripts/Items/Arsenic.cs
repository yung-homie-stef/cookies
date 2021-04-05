using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenic : Action
{
    public GameObject inventoryUI;
    public Player playerScript;
    public Krool_Aid kroolAidScript;

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
            kroolAidScript.poisoned = true;

            Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
            Destroy(_inventory.playerInventoryItems[itemIndex]);
            Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

            inventoryUI.GetComponent<Inventory_UI>().DisableUI();
        }
    }
}
