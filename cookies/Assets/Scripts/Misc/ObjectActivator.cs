using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject[] objects;
    public bool[] activeOrNot;
    public bool isDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(activeOrNot[i]);
        }

        if (isDestroyed)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
