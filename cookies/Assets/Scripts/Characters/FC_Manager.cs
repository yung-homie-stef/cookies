using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Manager : MonoBehaviour
{
    private Tags _pornoTag;
    private GameObject _potentialPornoMag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            _potentialPornoMag = other.gameObject;
            _pornoTag = _potentialPornoMag.GetComponent<Tags>();

            for (int i =0; i < _pornoTag.tags.Length; i++)
            {
                if (_pornoTag.tags[i] == "Porn")
                {
                    Debug.Log("don't mind if i do");
                    // call function to go to magazine
                    break;
                }
            }

        }
    }
}
