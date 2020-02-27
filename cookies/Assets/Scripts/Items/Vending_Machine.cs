using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vending_Machine : Interactable
{
    public GameObject candyBar;

    private Animator _animator;
    private bool _hasVended;

    // Start is called before the first frame update
    void Start()
    {
        _hasVended = false;
        _animator = gameObject.GetComponent<Animator>();   
    }

    public override void Interact()
    {
        if (_hasVended == false)
        {
            _animator.SetBool("vending", true);
            candyBar.GetComponent<BoxCollider>().enabled = true;
            _hasVended = true;
        }
    }
}
