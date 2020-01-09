using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquirableInteractable : Interactable
{
    public Vector3 zoomScale;
    public Transform zoomedInTransform;
    public RuntimeAnimatorController controller;

    private GameObject _duplicate;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void Interact()
    {
        // create a duplicate of the acquirable interactable object that appears on the screen zoomed in 
        // to show the player they've picked it up
        _duplicate = Instantiate(gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
        _duplicate.transform.localScale = zoomScale;
        _duplicate.GetComponent<Interactable>().enabled = false;

        _animator = _duplicate.AddComponent<Animator>();
        _animator.runtimeAnimatorController = controller;

        // setting duplicate object to the zoomed-in object's layer
        _duplicate.layer = 8;
    }

}