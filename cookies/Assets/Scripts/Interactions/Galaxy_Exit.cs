﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy_Exit : Interactable
{
    public GameObject altarDrugItem;
    public GameObject blackout;
    public bool huxleyThreadComplete = false;
    public Vector3 endOfThreadTransform;

    private Krool_Aid _kroolAidScript;
    private Animator _blackoutAnimator;

    // Start is called before the first frame update
    new void Start()
    {
        _kroolAidScript = altarDrugItem.GetComponent<Krool_Aid>();
        _blackoutAnimator = blackout.GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (huxleyThreadComplete)
            _kroolAidScript.preTeleportPosition = endOfThreadTransform;

        StartCoroutine(ActivateExit(5.0f));
        _blackoutAnimator.SetBool("faded", true);
    }

    private IEnumerator ActivateExit(float waitTime)
    { 
        yield return new WaitForSeconds(waitTime);

        _kroolAidScript.ReturnPlayerToEarth();

    }
}
