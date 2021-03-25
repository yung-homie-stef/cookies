using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red_Pills : Action
{
    public Text noticeText;
    public string newText;
    public GameObject inventoryUI;

    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    // Update is called once per frame
    public override void Use(int itemIndex)
    {

        _notice.ChangeText(newText, 6.0f);

        Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
        Destroy(_inventory.playerInventoryItems[itemIndex]);
        Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

        inventoryUI.GetComponent<Inventory_UI>().DisableUI();
    }

}
