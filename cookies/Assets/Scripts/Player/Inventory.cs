﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangeCallback;

    public int space = 20;
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    public GameObject playerInventoryIndicator;
    public GameObject[] inventoryUISlots;
    public GameObject[] playerInventoryItems;
    public bool[] isSlotFull;
    public bool isWeaponEquipped; // variable that dictates whether player can cycle through inventory items

    //[SerializeField]
    public static int currentSelectedSlot = 0;

    #region SINGLETON
    private void Awake()
    {
        if (instance !=null)
        {
            Debug.LogWarning("more than one instance of inventory found");
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        isWeaponEquipped = false;
    }

    private void Update()
    {
        #region OLD CODE
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
        #endregion
    }

    private void SelectItemInInventory(int currentSlot)
    {
        #region OLD CODE
        //// only allow a slot to be selectable if its full
        //if (isSlotFull[currentSlot])
        //{
        //    if (playerInventoryIndicator.activeSelf == false)
        //    {
        //        playerInventoryIndicator.SetActive(true);
        //    }

        //    Vector3 newPosition = new Vector3(playerInventoryIndicator.transform.position.x, inventoryUISlots[currentSlot].transform.position.y, 0);
        //    playerInventoryIndicator.transform.position = newPosition;
        //}
        #endregion
    }

    public int GetCurrentSlot()
    {
        return currentSelectedSlot;
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            return false;
        }

        items.Add(item);

        if (onItemChangeCallback != null)
        onItemChangeCallback.Invoke();


       
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangeCallback != null)
            onItemChangeCallback.Invoke();
       
    }



}
