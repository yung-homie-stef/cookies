using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public GameObject player;

    protected Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Use()
    {
        _inventory.isFull[_inventory.GetCurrentSlot()] = false;
        Destroy(_inventory.inventoryItems[_inventory.GetCurrentSlot()]);
    }
}
