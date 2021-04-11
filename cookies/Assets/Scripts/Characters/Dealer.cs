using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : Interactable
{
    public GameObject dialogueManager;
    public Set_of_Sentences[] sentenceSets;

    public Text hintText;

    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private string[] currentSentences;
    private int _dialogueValue;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InteractAction()
    {
        HandleDialogue(_dialogueValue);
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
        GetComponent<Animator>().SetBool("dead", true);
        GetComponent<BoxCollider>().enabled = false;
        hintText.text += "\n- GET OUT GET OUT GET OUT";
    }

}
