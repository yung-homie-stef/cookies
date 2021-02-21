using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public void EnableWeaponCollider(int flag)
    {
        if (flag == 1)
            gameObject.GetComponent<BoxCollider>().enabled = true;
        else if (flag == 2)
            gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        // deal damage
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("wallah");
        }
    }
}
