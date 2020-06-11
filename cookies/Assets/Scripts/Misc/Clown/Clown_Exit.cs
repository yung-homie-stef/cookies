using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown_Exit : MonoBehaviour
{
    public GameObject exit;

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
            _openable.newText = "THAT CREEPED LOCKED THE DOOR ON ME";
            _openable.reqType = Interactable.RequirementType.Single;
            _openable.requiredTags[0] = "Clown_Key"; 
        }
    }

}
