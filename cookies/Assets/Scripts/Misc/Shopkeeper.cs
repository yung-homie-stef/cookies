using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public GameObject[] storefrontColliders;

    private Tags _tags;
    private int _storeCredit;

    // Start is called before the first frame update
    void Start()
    {
        _storeCredit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_storeCredit == 0) // when the player has no money, block off any purchasable items
        {
            for (int i = 0; i < storefrontColliders.Length; i++)
            {
                storefrontColliders[i].SetActive(true);
            }
        }
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
                    Destroy(other.gameObject);
                    AllowPurchases();
                }
            }
        }
    }

    private void AllowPurchases()
    {
        _storeCredit++; // allows player to purchase items

        for (int i=0; i < storefrontColliders.Length; i++)
        {
            storefrontColliders[i].SetActive(false); // do this by disabling the boxes that block players from picking up objects
        }
    }
}
