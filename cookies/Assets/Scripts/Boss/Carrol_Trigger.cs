﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrol_Trigger : MonoBehaviour
{
    public GameObject kitchenDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // play carrol cutscene
            kitchenDoor.GetComponent<OpenableInteractable>().isLocked = true;
            kitchenDoor.GetComponent<OpenableInteractable>().newText = "THEY LOCKED THE DOOR ON ME";
            kitchenDoor.GetComponent<OpenableInteractable>().closeToggle = true;
        }
    }
}