using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float timeDelay = 2f;
    float startTimer;

    public int damage = 2;
    public float explosiveForce = 20f;
    public float explosiveRadius = 15f;

    public ParticleSystem explosionPrefab;
    private bool _hasBlownUp = false;

    // Update is called once per frame
    void Update()
    {
        startTimer += Time.deltaTime;
        if (startTimer >= timeDelay)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (!_hasBlownUp)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            _hasBlownUp = true;
        }

        Collider[] coll = Physics.OverlapSphere(transform.position, explosiveRadius);

        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].gameObject.GetComponent<Victim>())
            {
                coll[i].gameObject.GetComponent<Victim>().TakeDamage("explosive", 1);
                coll[i].gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, explosiveRadius); 
            }
        }

        Destroy(gameObject);
    }
}
