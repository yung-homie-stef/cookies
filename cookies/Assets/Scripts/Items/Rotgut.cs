using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotgut : Action
{
    public Camera VHSCamera;
    public AudioClip drinkSound;

    private bool _isDrunk = false;
    private Drunk _drunkScript;

    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _drunkScript = VHSCamera.gameObject.GetComponent<Drunk>();
    }

    public override void Use()
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            if (!_isDrunk)
            {
                GetComponent<AudioSource>().PlayOneShot(drinkSound);
                _drunkScript.enabled = true;

                _isDrunk = true;
                base.Use();
            }
        }
    }

}
