using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public GameObject[] storefrontColliders;
    public GameObject[] catalogue;

    private Tags _tags;
    private static int _storeCredit;

    // Start is called before the first frame update
    void Start()
    {
        _storeCredit = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable") // when the player drops an object to the shopkeeper, have him check if its currency, if so
        {
            _tags = other.GetComponent<Tags>();

            for (int i = 0; i < _tags.tags.Length; i++)
            {
                if (_tags.tags[i] == "Currency")
                {
                    _storeCredit++; // when store credit is above 0 players can buy items 
                    Destroy(other.gameObject);
                    SetBuyable(false);
                }
            }
        }
    }

    private void SetBuyable(bool condition)
    {
        for (int i = 0; i < storefrontColliders.Length; i++)
        {
            storefrontColliders[i].SetActive(condition); // do this by disabling the boxes that block players from picking up objects
        }
    }


    public void Purchase()
    {
        _storeCredit--;
        Debug.Log(_storeCredit);
        if (_storeCredit == 0)
        {
            SetBuyable(true);
        }
    }
}


