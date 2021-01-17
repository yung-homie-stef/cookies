using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Unlocker : MonoBehaviour
{
    public GameObject exit;
    public int reqTypeInt;
    public string[] reqTags;

    private OpenableInteractable _openable;

    // Start is called before the first frame update
    void Start()
    {
        _openable = exit.GetComponent<OpenableInteractable>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _openable.isLocked = false;
            _openable.isOpened = true;
            _openable.SetOpenToggle(1);
            _openable.SetCloseToggle(0);
            _openable.EnactOpening();


        }

        Destroy(gameObject);
    }
}
