﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Button : Interactable
{
    public GameObject shotgun;
    public GameObject victim;
    public GameObject intercom;
    public GameObject blood;
    public GameObject intercomCamera;
    public ParticleSystem muzzleFlash;

    private Rigidbody[] _childBodies;
    private bool _hasFired;

    // Start is called before the first frame update
    new void Start()
    {
        _childBodies = victim.GetComponentsInChildren<Rigidbody>();
    }

    public override void InteractAction()
    {
        if (!_hasFired)
        {
            GetComponent<Animator>().SetBool("pressed", true);
            shotgun.GetComponent<Animator>().SetBool("fired", true);
            intercom.GetComponent<BoxCollider>().enabled = true;
            intercomCamera.SetActive(true);
            victim.GetComponent<Animator>().SetBool("dead", true);
            muzzleFlash.Play();

            blood.SetActive(true);

            victim.GetComponent<Victim>().TakeDamage(victim.transform.position, shotgun.transform.forward);
            _hasFired = true;
        }

    }
}
