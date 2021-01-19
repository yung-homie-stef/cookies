using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manifesto : Action
{
    public GameObject manifesto_page;
    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use(int itemIndex)
    {
        player.GetComponent<Player>().DisableMovement();
        manifesto_page.SetActive(true);
        inventoryUI.GetComponent<Inventory_UI>().DisableUI();
    }
}
