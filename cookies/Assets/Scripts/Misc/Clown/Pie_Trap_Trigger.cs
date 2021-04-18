using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie_Trap_Trigger : MonoBehaviour
{
    public GameObject pieMechanism;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pieMechanism.GetComponent<Animator>().SetBool("triggered", true);
        }
    }
}
