using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    public GameObject player;
    public GameObject[] catalogue = new GameObject[4];
    public GameObject[] storage;
    public Transform[] storeSlots;
    List<GameObject> stock = new List<GameObject>(); // for objects not yet on display

    private Tags _tags;
    private Inventory _inventory;
    private static int _storeCredit;

    // Start is called before the first frame update
    void Start()
    { 
        _inventory = player.GetComponent<Inventory>();

        _storeCredit = 0;

        for (int i=0; i < storage.Length; i++)
        {
            stock.Add(storage[i]); // fill the list up with items that are not yet for sale
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
                    _storeCredit++; // when store credit is above 0 players can buy items 
                    Destroy(other.gameObject);
                    SetBuyable(true);
                }
            }
        }
    }

    private void SetBuyable(bool condition)
    {
        for (int i = 0; i < catalogue.Length; i++)
        {
            catalogue[i].GetComponent<BoxCollider>().enabled = condition; // do this by disabling the boxes that block players from picking up objects
        }
    }

    public override void Interact()
    {
        for (int i = 0; i < _inventory.UISlots.Length; i++)
        {
            if (_inventory.inventoryItems[i] != null)
            {
                _tags = _inventory.inventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Credit_Card")
                    {
                        _inventory.inventoryItems[i].GetComponent<Credit_Card>().Transaction();
                        SetBuyable(true);
                    }
                }
            }
        }
    }


    public void Purchase(int shelfNumber)
    {
        _storeCredit--;

        catalogue[shelfNumber] = null; // take this item off the store shelves
        catalogue[shelfNumber] = stock[0]; // replace it with next item in stock list
        catalogue[shelfNumber].SetActive(true); // make the item visible
        catalogue[shelfNumber].transform.position = storeSlots[shelfNumber].transform.position;

        stock.RemoveAt(0); // remove the new item fom storage

        Debug.Log(_storeCredit);
        if (_storeCredit == 0)
        {
            SetBuyable(false);
        }
    }
}


