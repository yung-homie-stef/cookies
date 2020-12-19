using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape_List : MonoBehaviour
{
    public GameObject[] tapes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableVHSTapes(int tapeIndex)
    {
        for (int i = 0; i < tapes.Length; i++)
        {
            if (i == tapeIndex)
            {
                tapes[i].gameObject.SetActive(true);
            }
            else
                tapes[i].gameObject.SetActive(false);
        }
    }

}
