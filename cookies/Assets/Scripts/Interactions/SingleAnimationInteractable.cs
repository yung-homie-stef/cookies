using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationInteractable : Interactable
{
    public GameObject toast;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public override void InteractAction()
    {
        _animator.SetBool("interacted", true);
        toast.GetComponent<BoxCollider>().enabled = true;
    }
}
