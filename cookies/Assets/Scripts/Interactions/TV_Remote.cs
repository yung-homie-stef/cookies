using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Remote : Interactable
{
    public GameObject holdingCellTV;
    public GameObject report;

    // Start is called before the first frame update
    new void Start()
    {
        
    }

    public override void InteractAction()
    {
        holdingCellTV.tag = "Interactable";
        holdingCellTV.GetComponent<Interactable>().InteractAction();
        report.SetActive(true);
    }
}
