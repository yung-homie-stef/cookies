using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public GameObject playerInventoryIndicator;
    public GameObject[] inventoryUISlots;
    public GameObject[] playerInventoryItems;
    public bool[] isSlotFull;
    public bool isWeaponEquipped; // variable that dictates whether player can cycle through inventory items

    //[SerializeField]
    public static int currentSelectedSlot = 0;

    private void Start()
    {
        isWeaponEquipped = false;
    }

    private void Update()
    {
        if (isWeaponEquipped == false) // when an object that takes up the player's hand is used, prohibit them from being able to select and use other items
        {
            if (Input.GetButton("FirstSlot"))
            {
                currentSelectedSlot = 0;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("SecondSlot"))
            {
                currentSelectedSlot = 1;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("ThirdSlot"))
            {
                currentSelectedSlot = 2;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("FourthSlot"))
            {
                currentSelectedSlot = 3;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("FifthSlot"))
            {
                currentSelectedSlot = 4;
                SelectItemInInventory(currentSelectedSlot);
            }
        }
    }

    private void SelectItemInInventory(int currentSlot)
    {
        // only allow a slot to be selectable if its full
        if (isSlotFull[currentSlot])
        {
            if (playerInventoryIndicator.activeSelf == false)
            {
                playerInventoryIndicator.SetActive(true);
            }

            Vector3 newPosition = new Vector3(playerInventoryIndicator.transform.position.x, inventoryUISlots[currentSlot].transform.position.y, 0);
            playerInventoryIndicator.transform.position = newPosition;
        }
    }

    public int GetCurrentSlot()
    {
        return currentSelectedSlot;
    }



}
