using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : Action
{
    public GameObject salvador;
    public ParticleSystem shroomSmoke;
    public AudioClip smokeSFX;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            GetComponent<AudioSource>().PlayOneShot(smokeSFX);
            shroomSmoke.Play(); // create a puff of smoke for salvador to appear in
          
            salvador.SetActive(true);
        }
    }

}
