using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : Action
{
    public GameObject salvador;
    public ParticleSystem shroomSmoke;
   

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        shroomSmoke.Play(); // create a puff of smoke for salvador to appear in
        
        salvador.SetActive(true);
        base.Use();
    }

}
