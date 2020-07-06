using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Button : Interactable
{
    public GameObject shotgun;
    public GameObject victim;

    private Rigidbody[] _childBodies;

    // Start is called before the first frame update
    new void Start()
    {
        _childBodies = victim.GetComponentsInChildren<Rigidbody>();
    }

    public override void InteractAction()
    {
        shotgun.GetComponent<Animator>().SetBool("fired", true);
        victim.GetComponent<Animator>().SetBool("dead", true);

        foreach (var body in _childBodies)
        {
            body.isKinematic = false;
        }

    }
}
