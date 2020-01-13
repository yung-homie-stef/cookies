using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationInteractable : Interactable
{
    public string itemNeeded;
    public GameObject player;

    private bool interacted;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted)
            _animator.SetBool("interacted", true);
    }

    public override void Interact()
    {
        // if current equipped item is the same as the item needed to interact with, do stuff
    }
}
