﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquirableInteractable : Interactable
{
    public Vector3 originalScale;
    public Vector3 zoomScale;
    public Transform zoomedInTransform;
    public RuntimeAnimatorController controller;

    public Text itemName;
    public Text itemDesc;

    public string nameString;
    public string descString;

    public GameObject textCanvas;
    public GameObject player;
    public GameObject VHS_Camera;

    private GameObject _duplicate;
    private IEnumerator _removeCoroutine;
    private bool _clickable;
    private Movement _movement;
    private CameraController _camControlller;
    private Collider _collider;
    private Inventory _inventory;
    private Pickup _pickup;

    // Start is called before the first frame update
    void Start()
    {
        _removeCoroutine = RemoveAcquirable(2.0f);
        _clickable = false;
        originalScale = gameObject.transform.localScale;

        _movement = player.GetComponent<Movement>();
        _camControlller = VHS_Camera.GetComponent<CameraController>();
        _collider = gameObject.GetComponent<Collider>();
        _inventory = player.GetComponent<Inventory>();
        _pickup = gameObject.GetComponent<Pickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_clickable == true)
            {
                _pickup.AddItem();

                textCanvas.SetActive(false);

                Destroy(_duplicate);

                _movement.enabled = true;
                _camControlller.enabled = true;
                _clickable = false;
            }
        }

    }

    public override void Interact()
    {
        // create a duplicate of the acquirable interactable object that appears on the screen zoomed in 
        // to show the player they've picked it up
        _duplicate = Instantiate(gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
        _duplicate.transform.localScale = zoomScale;
        _duplicate.GetComponent<Interactable>().enabled = false;

        _collider.enabled = false;

        // play rotating animation
        _animator = _duplicate.AddComponent<Animator>();
        _animator.runtimeAnimatorController = controller;

        // stop the player from moving
        _movement.enabled = false;
        _camControlller.enabled = false;

        textCanvas.SetActive(true);
        ChangeText(nameString, descString);

        // setting duplicate object to the zoomed-in object's layer
        _duplicate.layer = 8;
        // after a 2 second delay allow the player to click away
        StartCoroutine(_removeCoroutine);
    }

    public void Drop()
    {
        gameObject.layer = 0;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.localScale = originalScale;
        _inventory.isFull[Inventory.currentSlot] = false;
    }

    private void ChangeText(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;
    }

    private IEnumerator RemoveAcquirable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _clickable = true;
    }

}