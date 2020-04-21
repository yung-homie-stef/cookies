using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confessional : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public GameObject huxley;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Father_Huxley _huxley;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = GetComponent<Animator>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _huxley = huxley.gameObject.GetComponent<Father_Huxley>();
    }

    public override void Interact()
    {
        HandleDialogue(0); // confesisonal window only has 1
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
        _huxley.ConfirmTaskCompleted();
    }
}
