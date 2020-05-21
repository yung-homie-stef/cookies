using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    public GameObject coconutCreme;
    public ParticleSystem pieCreamParticle;
    public bool hasAlreadySwung = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (!hasAlreadySwung)
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(coconutCreme);
                //pieCreamParticle.Play();
                //play splat sound
            }
        }
    }
}
