using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Player playerScript;

    public Victim selectedBoss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript.TakeDamage();

            if (Player.voodoo)
            {
                selectedBoss.TakeDamage("voodoo", 1);
            }
        }
    }
}
