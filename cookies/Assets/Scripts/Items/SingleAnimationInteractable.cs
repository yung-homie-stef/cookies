using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationInteractable : Interactable
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public override void Interact()
    {
        _animator.SetBool("interacted", true);
    }
}
