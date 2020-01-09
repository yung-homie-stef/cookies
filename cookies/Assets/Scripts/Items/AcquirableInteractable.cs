using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquirableInteractable : Interactable
{
    public Vector3 zoomScale;
    public Transform zoomedInTransform;

    private GameObject _duplicate;

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
        _duplicate = Instantiate(this.gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
        _duplicate.transform.localScale = zoomScale;
        _duplicate.GetComponent<Interactable>().enabled = false;
        // setting duplicate object to the zoomed-in object's layer
        _duplicate.layer = 8;
    }

}