using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manifesto_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("Fire1")))
        {
            gameObject.SetActive(false);
        }
    }
}
