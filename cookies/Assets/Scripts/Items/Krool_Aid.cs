﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krool_Aid : Action
{
    public GameObject galaxyGroup;
    public GameObject blackout;
    public Transform galaxyTransform;
    public GameObject teleportParticle;
    public GameObject Samet;
    public GameObject UFO;
    public AudioClip sipSFX;

    [HideInInspector]
    public Vector3 preTeleportPosition;

    private Animator _animator;
    private Tags _tags;
    private bool hasTeleported;
    private Samet _sametScript;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        hasTeleported = false;
        _animator = blackout.GetComponent<Animator>();
        _sametScript = Samet.GetComponent<Samet>();
        _inventory = player.GetComponent<Inventory>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public override void Use(int itemIndex)
    {

        if (!hasTeleported)
        {
            StartCoroutine(DrugsKickIn(5.0f));
            preTeleportPosition = player.transform.position;
            hasTeleported = true;
            _audioSource.PlayOneShot(sipSFX);

            for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
            {
                if (_inventory.playerInventoryItems[i] != null)
                {
                    _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                    for (int j = 0; j < _tags.tags.Length; j++)
                    {
                        if (_tags.tags[j] == "Rat Posion")
                        {
                            Audio_Manager.globalAudioManager.PlaySound("ping", Audio_Manager.globalAudioManager.intangibleSoundArray);
                            _sametScript.hasTranslated = true;


                            _inventory.isSlotFull[i] = false;
                            Destroy(_inventory.playerInventoryItems[i]);
                            break;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator DrugsKickIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // teleport player to galaxy 
        player.transform.position = galaxyTransform.position;
        player.transform.rotation = Quaternion.Euler(0, -180, 0);

        // stops player from teleporting to space if they are already in space
        hasTeleported = true;
        _animator.SetBool("faded", false);

        teleportParticle.SetActive(true);
        Samet.SetActive(true);
        UFO.SetActive(true);

        gameObject.GetComponent<AcquirableInteractable>().canDrop = false; // so as to not lose krool aid in space and softlock the questline

    }

    public void ReturnPlayerToEarth()
    {
        _animator.SetBool("faded", false);
        player.transform.position = preTeleportPosition;
        hasTeleported = false;


        StartCoroutine(AllowPlayerToDropKroolAid(6.2f));
    }

    private IEnumerator AllowPlayerToDropKroolAid(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<AcquirableInteractable>().canDrop = true;

    }
}
