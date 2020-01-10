using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int inventorySize = 6;
    public AcquirableInteractable[] items = new AcquirableInteractable[inventorySize];

    public void AddInventoryItem(AcquirableInteractable itemToAdd)
    {
        for (int i = 0; i< items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return;
            }
        }
    }

}
