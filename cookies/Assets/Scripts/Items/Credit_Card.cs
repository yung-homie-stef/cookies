using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit_Card : MonoBehaviour
{
    public Text noticeText;
    public GameObject player;

    private int allowedPurchases;
    private Notice _notice;
    private Tags _tags;
    private Inventory _inventory;

    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        allowedPurchases = 3; // player can buy 3 items before being overdrafted
    }


    public void Transaction()
    {
        allowedPurchases--;

        if (allowedPurchases > 0)
        {
            _notice.ChangeText("CREDIT CARD SUCCESSFULLY USED"); // let player know credit card was used
        }
        else if (allowedPurchases == 0) // when player runs out of purchases destroy the card
        {
            Overdraft();   
        }
    }

    private void Overdraft()
    {
        Destroy(gameObject);
        _notice.ChangeText("CREDIT CARD OVERDAFTED");

        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Credit_Card")
                    {
                        _inventory.isSlotFull[i] = false; // empty slot that credit card was in
                    }
                }
            }
        }
    }
}
