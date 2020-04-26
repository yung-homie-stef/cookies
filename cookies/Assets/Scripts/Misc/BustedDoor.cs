using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustedDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Hitbox")
        {
            GetComponent<Animator>().SetBool("is_opened", true);
            GetComponent<OpenableInteractable>().isLocked = false;
            GetComponent<OpenableInteractable>().PlayDoorSound(4);
        }
    }
}
