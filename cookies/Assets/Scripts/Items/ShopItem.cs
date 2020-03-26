﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : AcquirableInteractable
{
    public GameObject shopkeeperCharacter;

    private Shopkeeper _shopKeeper;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        _shopKeeper = shopkeeperCharacter.GetComponent<Shopkeeper>();
    }

    public override void Interact()
    {
        base.Interact();
        _shopKeeper.Purchase();
    }
}
