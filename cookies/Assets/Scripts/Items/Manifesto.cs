using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manifesto : Action
{
    public GameObject manifesto_page;
    public GameObject inventoryUI;

    public Text hintText;

    private bool _hasHinted = false;

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

        if (!_hasHinted)
        {
            hintText.text += "\n- 311";
            _hasHinted = true;
        }
    }
}
