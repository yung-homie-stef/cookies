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

            switch (reqTypeInt)
            {
                case 0:
                    _openable.reqType = Interactable.RequirementType.None;
                    break;
                case 1:
                    _openable.reqType = Interactable.RequirementType.Single;
                    break;
                case 2:
                    _openable.reqType = Interactable.RequirementType.List;
                    break;
            }

            _openable.newText = updatedText;
            _openable.isOpened = false;
            _openable.SetOpenToggle(0);
            _openable.SetCloseToggle(1);
            _openable.EnactOpening();
            _openable.isLocked = true;
            

            if (_openable.reqType == Interactable.RequirementType.Single || _openable.reqType == Interactable.RequirementType.List)

                for (int i = 0; i < reqTags.Length; i++)
                {
                    _openable.requiredTags[i] = reqTags[i];
                }
            
        }

        Destroy(gameObject);
    }

}
