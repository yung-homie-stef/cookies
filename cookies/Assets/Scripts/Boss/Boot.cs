using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    public float force = 0.0f;

 
    public void EnableBootCollider(int flag)
    {
        if (flag == 1)
            gameObject.GetComponent<BoxCollider>().enabled = true;
        else if (flag == 2)
            gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

        }
    }
}
