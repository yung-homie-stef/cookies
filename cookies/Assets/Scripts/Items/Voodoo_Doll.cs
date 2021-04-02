using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voodoo_Doll : Action
{
    public GameObject inventoryUI;
    public Player playerScript;
    public string newText;

    private Movement _movement;
    public Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            Player.voodoo = true;
            _notice.ChangeText(newText, 8.0f);

            Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
            Destroy(_inventory.playerInventoryItems[itemIndex]);
            Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

            inventoryUI.GetComponent<Inventory_UI>().DisableUI();
        }
    }
}
