﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class OpenableInteractable : Interactable
{
    public GameObject player;
    public string requiredKey;
    public string newText;
    public bool isLocked;
    public Text noticeText;
    public bool isOpened;
    public bool openToggle = true;
    public bool closeToggle = false;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        isOpened = false;
        _animator = GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public void EnactOpening()
    {
        if (isOpened == true)
        {
            if (openToggle)
            {
                // play the opening animation
                _animator.SetBool("is_opened", true);
                PlayDoorSound(0);
            }

        }

        else if (isOpened == false)
        {
            if (closeToggle)
            {
                _animator.SetFloat("animSpeed", 1);
                _animator.SetBool("is_opened", false);
                PlayDoorSound(1);
            }
        }
    }

    public override void InteractAction()
    {
       isOpened = !isOpened; // only enact opening if animation isnt playing
       EnactOpening();    
    }
    

    public void PlayDoorSound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(Door_Sound_Effects.globalDoorSounds.doorSFX[clip]);
    }

    public void SetOpenToggle(int cond)
    {
        if (cond == 0)
            openToggle = false;

        else if (cond == 1)
            openToggle = true;
    }

    public void SetCloseToggle(int cond)
    {
        if (cond == 0)
            closeToggle = false;

        else if (cond == 1)
            closeToggle = true;
    }

}
