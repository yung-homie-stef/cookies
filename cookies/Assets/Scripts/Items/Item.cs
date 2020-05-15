using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 2)]

public class Item : ScriptableObject
{
    public string itemName = "NAME";
    public string itemDesc = "DESC";
    public Sprite icon = null;
    public GameObject OG = null;
    public bool usable;

    public virtual void Use()
    {
        if (usable)
        {
            //int i = Inventory.instance.items.IndexOf[GameObject];

            //if (Inventory.instance.playerInventoryItems[i].GetComponent<Action>())
            //{
            //    Inventory.instance.playerInventoryItems[i].GetComponent<Action>().Use();
            //}
        }
        
    }
}
