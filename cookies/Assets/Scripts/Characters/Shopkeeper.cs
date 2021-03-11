using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    public GameObject player;
    public string[] sentences;
    public GameObject dialogueManager;
    public Set_of_Sentences[] sentenceSets;
    public List <GameObject> stock = new List<GameObject>();

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

    }

    public override void InteractAction()
    {
        if (!_hasSpoken)
        {
            HandleDialogue(0);
        }
        else
        {
            _storeCredit++;
            if (_storeCredit == 1)
            {
                UnlockShelves();
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
        reqType = RequirementType.Single;
        requiredTags = new string[1];
        requiredTags[0] = "Currency";
    }

    void UnlockShelves()
    {
        foreach (GameObject items in stock)
        {
            items.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Purchase(GameObject transaction)
    {
        _storeCredit--;
        stock.Remove(transaction);

        if (_storeCredit == 0)
        {
            foreach (GameObject items in stock)
            {
                items.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}


