using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float timeDelay = 2f;
    float startTimer;
    public ParticleSystem smokeScreenPrefab;
    private bool _hasBlownUp = false;

    // Start is called before the first frame update
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
            Instantiate(smokeScreenPrefab, transform.position, transform.rotation);
            _hasBlownUp = true;
        }
        Destroy(gameObject);
    }

}