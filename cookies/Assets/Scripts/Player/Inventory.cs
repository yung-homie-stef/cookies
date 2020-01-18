﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryIndicator;
    public GameObject[] UISlots;
    public GameObject[] inventoryItems;
    public bool[] isFull;

    [SerializeField]
    public static int currentSlot;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetButton("FirstSlot"))
        {
            currentSlot = 0;
            SelectItemInInventory(currentSlot);
        }
        else if (Input.GetButton("SecondSlot"))
        {
            currentSlot = 1;
            SelectItemInInventory(currentSlot);
        }
        else if (Input.GetButton("ThirdSlot"))
        {
            currentSlot = 2;
            SelectItemInInventory(currentSlot);
        }
        else if (Input.GetButton("FourthSlot"))
        {
            currentSlot = 3;
            SelectItemInInventory(currentSlot);
        }
        else if (Input.GetButton("FifthSlot"))
        {
            currentSlot = 4;
            SelectItemInInventory(currentSlot);
        }
    }

    private void SelectItemInInventory(int currentSlot)
    {
        // only allow a slot to be selectable if its full
        if (isFull[currentSlot])
        {
            if (inventoryIndicator.activeSelf == false)
            {
                inventoryIndicator.SetActive(true);
            }

            Vector3 newPosition = new Vector3(inventoryIndicator.transform.position.x, UISlots[currentSlot].transform.position.y, 0);
            inventoryIndicator.transform.position = newPosition;
        }
    }



}
