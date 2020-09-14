using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject[] objects;
    public bool[] activeOrNot;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(activeOrNot[i]);
        }

        Destroy(gameObject);
    }
}
