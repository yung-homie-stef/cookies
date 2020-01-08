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
        _duplicate = Instantiate(gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
    }

    public override void ReactToPlayerInteraction()
    {
        _duplicate = Instantiate(gameObject, zoomedInTransform.position, zoomedInTransform.rotation);
        _duplicate.transform.localScale = zoomScale;
        // setting duplicate object to the zoomed-in object's layer
        _duplicate.layer = 8;
    }

}