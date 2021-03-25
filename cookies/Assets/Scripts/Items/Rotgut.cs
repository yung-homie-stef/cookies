using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotgut : Action
{
    public Camera VHSCamera;
    public AudioClip drinkSound;
    public GameObject inventoryUI;

    private bool _isDrunk = false;
    private Drunk _drunkScript;

    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _drunkScript = VHSCamera.gameObject.GetComponent<Drunk>();
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            if (!_isDrunk)
            {
                _drunkScript.enabled = true;
                _drunkScript.BeginSoberCountdown();

                _isDrunk = true;

                Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
                Destroy(_inventory.playerInventoryItems[itemIndex]);
                Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);


                inventoryUI.GetComponent<Inventory_UI>().DisableUI();

            }
        }
    }

}
