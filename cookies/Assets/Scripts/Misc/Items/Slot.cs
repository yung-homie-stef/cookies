using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Item item;
    //public Image icon;
    public Button removeButton;
    public Text itemNameHeader;
    public Image usableOutline;


    public void AddItem(Item newItem)
    {
        item = newItem;
        removeButton.interactable = true;

        if (item.usable == true)
        {
            usableOutline.enabled = true;
        }
        else
        {
            usableOutline.enabled = false;
        }

    }

    public void ClearSlot()
    {
            item = null;
            removeButton.interactable = false;
            usableOutline.enabled = false;
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
