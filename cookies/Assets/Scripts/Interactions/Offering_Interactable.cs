using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offering_Interactable : Interactable
{
    public GameObject itemToGive;

    public override void Interact()
    {
        itemToGive.GetComponent<AcquirableInteractable>().Interact();
    }
}
