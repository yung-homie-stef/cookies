using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Uses : MonoBehaviour
{
    public GameObject player;
    public int uses;

    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public void UseKey(int currentItemSlot)
    {
        uses--;

        if (uses == 0)
        {
            _inventory.isSlotFull[currentItemSlot] = false;
            Destroy(_inventory.playerInventoryItems[currentItemSlot]);
            Inventory.instance.items.RemoveAt(currentItemSlot);
        }
    }

   
}
