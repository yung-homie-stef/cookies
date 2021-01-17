using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Outage : MonoBehaviour
{
    private GameObject[] lights;

    // Start is called before the first frame update
    private void Awake()
    {
        lights = GameObject.FindGameObjectsWithTag("ToggledLight");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
        }
    }
}
