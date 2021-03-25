﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : Action
{
    public GameObject salvador;
    public GameObject inventoryUI;
    public ParticleSystem shroomSmoke;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            shroomSmoke.Play(); // create a puff of smoke for salvador to appear in
          
            salvador.SetActive(true);

            Inventory.instance.inventoryUIScript.slots[itemIndex].SetHeaderToBlank();
            Destroy(_inventory.playerInventoryItems[itemIndex]);
            Inventory.instance.RemoveItem(Inventory.instance.items[itemIndex]);

            inventoryUI.GetComponent<Inventory_UI>().DisableUI();
        }
    }

}
