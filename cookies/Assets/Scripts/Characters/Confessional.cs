using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confessional : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject Huxley;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Inventory _inventory;
    private bool _hasStrippedPlayer = false;
    private Father_Huxley _huxleyScript;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _inventory = player.GetComponent<Inventory>();
        _huxleyScript = Huxley.GetComponent<Father_Huxley>();
    }

    public override void Interact()
    {
        if (!_hasStrippedPlayer)
        HandleDialogue(0);
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, true);
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
        if (!_hasStrippedPlayer)
        {
            StripPlayer();
            _huxleyScript.ConfirmTaskCompleted();
            _hasStrippedPlayer = true;
        }
    }

    void StripPlayer() // empty players inventory
    {
        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _inventory.isSlotFull[i] = false;
                Destroy(_inventory.playerInventoryItems[i]);
            }
        }
    }
}
