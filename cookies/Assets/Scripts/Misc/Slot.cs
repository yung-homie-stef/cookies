using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Item item;
    //public Image icon;
    public Button removeButton;
    public Text itemNameHeader;

    public void AddItem(Item newItem)
    {
        item = newItem;
        //icon.sprite = item.icon;
        //icon.enabled = true;
        removeButton.interactable = true;

    }

    public void ClearSlot()
    {
        item = null;
        //icon.sprite = null;
        //icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
       
        if (item != null)
        {
          
            int index = Inventory.instance.items.IndexOf(item);
            if (Inventory.instance.playerInventoryItems[index])
            {
                Debug.Log("watto did nothing wrong");
                Inventory.instance.UseItem(index); 
            }
        }
    }

    public void SetHeaderToItemName()
    {
        if (item != null)
        {
            itemNameHeader.text = item.itemName;
        }
    }

    public void SetHeaderToBlank()
    {
        if (item != null)
        {
            itemNameHeader.text = "";
        }
    }
}
