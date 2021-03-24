using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steroids : Action
{
    public Text noticeText;
    public string newText;
    public GameObject inventoryUI;

    private Player _playerScript;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
        _playerScript = player.GetComponent<Player>();
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            _playerScript.isRoided = true; // allow player to punch with this bool
            _notice.ChangeText(newText, 6.0f);

            Destroy(_inventory.playerInventoryItems[itemIndex]);
            Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

            inventoryUI.GetComponent<Inventory_UI>().DisableUI();
        }
    }
}
