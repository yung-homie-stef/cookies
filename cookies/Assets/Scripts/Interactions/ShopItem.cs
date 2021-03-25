using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Interactable
{
    public Shopkeeper _shopKeeper;

    public override void InteractAction()
    {
        _shopKeeper._storeCredit--;
        _shopKeeper.Purchase(gameObject);
        Destroy(this);
    }

}
