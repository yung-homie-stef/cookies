using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Outage : MonoBehaviour
{
    private GameObject[] lights;
    public Custodian mel;
    public Clown wayne;

    // Start is called before the first frame update
    private void Awake()
    {
        lights = GameObject.FindGameObjectsWithTag("ToggledLight");
    }

    private void OnTriggerEnter(Collider other)
    {
        wayne.gameObject.SetActive(false);
        wayne.gameObject.SetActive(false);

        if (other.tag == "Player")
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
        }


    }
}
