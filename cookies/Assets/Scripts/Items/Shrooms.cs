using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : Action
{
    public GameObject salvador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        salvador.SetActive(true); // hallucinate and make salvador appear
    }
}
