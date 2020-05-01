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
    public string[] sentences;
    public GameObject dialogueManager;
    public Set_of_Sentences[] sentenceSets;

    private Tags _tags;
    private Inventory _inventory;
    private static int _storeCredit;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private bool _hasSpoken;

    // Start is called before the first frame update
    new void Start()
    { 
        _inventory = player.GetComponent<Inventory>();
        eventHappensWhenTalkingIsDone = true;
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _hasSpoken = false;

        _storeCredit = 0;

        for (int i=0; i < storage.Length; i++)
        {
            stock.Add(storage[i]); // fill the list up with items that are not yet for sale
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_hasSpoken)
        {
            if (other.tag == "Interactable") // when the player drops an object to the shopkeeper, have him check if its currency, if so
            {
                if (_tags = other.GetComponent<Tags>())
                {
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
        if (!_hasSpoken)
        {
            HandleDialogue(0);
        }
        else
            CheckForCreditCard();

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

    private void CheckForCreditCard()
    {
        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Credit_Card")
                    {
                        _inventory.playerInventoryItems[i].GetComponent<Credit_Card>().Transaction();
                        SetBuyable(true);
                    }
                }
            }
        }
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, eventHappensWhenTalkingIsDone);
    }

    private string[] UpdateDialogue(string[] lines)
    {
        string[] sentences = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            sentences[i] = lines[i];
        }
        return sentences;
    }

    public override void ConversationEndEvent()
    {
        eventHappensWhenTalkingIsDone = false;
        _hasSpoken = true;
    }
}


