using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown_Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ouch owie that hurt");
        }
    }
}
