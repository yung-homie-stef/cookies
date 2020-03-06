﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableInteractable : Interactable
{
    public GameObject[] housedContents;
    private bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        _animator = GetComponent<Animator>();
    }

    public void EnactOpening()
    {
        if (isOpened == true)
        {
            // play the opening animation
            _animator.SetBool("is_opened", true);

            // set all objects that were hid in said container (if it's a cabinet or box etc.)
            // to active only after it is opened, this is to stop players from picking up objects
            // before opening their respective containers
            for (int i = 0; i < housedContents.Length; i++)
            {
                housedContents[i].SetActive(true);
                housedContents[i].GetComponent<BoxCollider>().enabled = true;
            }
        }

        else if (isOpened == false)
        {
            _animator.SetBool("is_opened", false); ;

            for (int i = 0; i < housedContents.Length; i++)
            {
                housedContents[i].SetActive(false);
                housedContents[i].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public override void Interact()
    {
        isOpened = !isOpened;
        Debug.Log(isOpened);
        EnactOpening();
    }
}