using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Lust : Interactable
{
    public GameObject dialogueManager;
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;

    [SerializeField]
    public int dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Inventory _inventory;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        dialogueValue = 0;
        _inventory = player.GetComponent<Inventory>();
        eventHappensWhenTalkingIsDone = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        HandleDialogue(dialogueValue);
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

    }
}
