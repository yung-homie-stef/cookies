using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentActivator : MonoBehaviour
{
    public GameObject[] itemsWithComponents = null;
    public string[] componentNames = null;
    public bool[] isComponentEnabled = null;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < itemsWithComponents.Length; i++)
        {
            (itemsWithComponents[i].GetComponent(componentNames[i]) as MonoBehaviour).enabled = isComponentEnabled[i];
        }
    }
}

