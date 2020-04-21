using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy_Exit : Interactable
{
    public GameObject altarDrugItem;
    public GameObject blackout;

    private Krool_Aid _kroolAidScript;
    private Animator _blackoutAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _kroolAidScript = altarDrugItem.GetComponent<Krool_Aid>();
        _blackoutAnimator = blackout.GetComponent<Animator>();
    }

    public override void Interact()
    {
        StartCoroutine(ActivateExit(5.0f));
        _blackoutAnimator.SetBool("faded", true);
    }

    private IEnumerator ActivateExit(float waitTime)
    { 
        yield return new WaitForSeconds(waitTime);

        _kroolAidScript.ReturnPlayerToEarth();

    }
}
