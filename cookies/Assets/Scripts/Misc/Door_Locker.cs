using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locker : MonoBehaviour
{
    public GameObject exit;
    public int reqTypeInt;
    public string updatedText;
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
            _openable.isLocked = true;
            _openable.newText = updatedText;
            _openable.isOpened = false;
            _openable.SetOpenToggle(0);
            _openable.SetCloseToggle(1);
            _openable.EnactOpening();
            this.enabled = false;


        }

        
    }

}
