using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Player playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript.TakeDamage();
        }
    }
}
