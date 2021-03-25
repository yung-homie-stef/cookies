using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butch : Interactable
{
    private AudioSource aSource;

    new void Start()
    {
        aSource = gameObject.GetComponent<AudioSource>();
    }

    public override void InteractAction()
    {
        if (aSource.enabled)
        aSource.enabled = false;
    }

}
