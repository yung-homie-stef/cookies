﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchasable : Interactable
{
    public Vector3 originalScale;
    public Vector3 originalRotation;
    public Vector3 zoomScale;
    public Transform zoomedInTransform;
    public RuntimeAnimatorController controller;
    public Image cursorImage;
    public Item itemScriptableObj;
    public Shopkeeper shopMans;

    public Text itemName;
    public Text itemDesc;

    public GameObject textCanvas;
    public GameObject player;
    public GameObject VHS_Camera;
    public bool canNowUse = false;
    public bool canDrop = true;
    public bool purchased = false;

    protected GameObject _duplicate;
    protected IEnumerator _removeCoroutine;
    protected bool _clickable;
    protected bool _hasHadDuplicate;
    protected Movement _movement;
    protected CameraController _camControlller;
    protected Collider _collider;
    protected Inventory _inventory;
    protected Pickup _pickup;

    // Start is called before the first frame update
    override protected void Start()
    {
        _removeCoroutine = RemoveAcquirable(1.0f);
        _clickable = false;
        _hasHadDuplicate = false;
        originalScale = gameObject.transform.localScale;
        originalRotation = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        _movement = player.GetComponent<Movement>();
        _camControlller = VHS_Camera.GetComponent<CameraController>();
        _collider = gameObject.GetComponent<Collider>();
        _inventory = player.GetComponent<Inventory>();
        _pickup = gameObject.GetComponent<Pickup>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_clickable == true)
            {
                textCanvas.SetActive(false);

                Destroy(_duplicate);

                _movement.enabled = true;
                _camControlller.enabled = true;
                cursorImage.enabled = true;
                canNowUse = true;

                _clickable = false;
            }
        }

    }

    public override void InteractAction()
    {
        Debug.Log("nuts");

        if (_hasHadDuplicate == false)
        {
            cursorImage.enabled = false;

            // create a duplicate of the acquirable interactable object that appears on the screen zoomed in 
            // to show the player they've picked it up
            _duplicate = Instantiate(gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
            _duplicate.transform.localScale = zoomScale;
            _duplicate.GetComponent<Interactable>().enabled = false;


            // play rotating animation
            _animator = _duplicate.AddComponent<Animator>();
            _animator.runtimeAnimatorController = controller;

            Debug.Log(_movement);
            // stop the player from moving
            _movement.enabled = false;
            _camControlller.enabled = false;
            textCanvas.SetActive(true);
            ChangeText(itemScriptableObj.itemName, itemScriptableObj.itemDesc);

            // setting duplicate object to the zoomed-in object's layer
            _duplicate.layer = 8;
            // after a 2 second delay allow the player to click away
            StartCoroutine(_removeCoroutine);

            if (purchased == false)
            {
                shopMans._storeCredit--;
                shopMans.Purchase(gameObject);
            }

            _hasHadDuplicate = true;
            purchased = true;

            if (_pickup)
                _pickup.AddItem(itemScriptableObj);

            Audio_Manager.globalAudioManager.PlaySound("pickup", Audio_Manager.globalAudioManager.intangibleSoundArray);

        }
        else
        {
            if (_pickup)
                _pickup.AddItem(itemScriptableObj);
        }
    }

    public void Drop()
    {
        if (canDrop)
        {
            gameObject.transform.localScale = originalScale;

            Vector3 dropPosition = new Vector3(player.transform.position.x, player.transform.position.y + GetComponent<Renderer>().bounds.size.y, player.transform.position.z);

            gameObject.layer = 0;
            gameObject.transform.position = dropPosition;
            gameObject.transform.eulerAngles = originalRotation;
        }
    }

    protected void ChangeText(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;
    }

    protected IEnumerator RemoveAcquirable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _clickable = true;
    }

}