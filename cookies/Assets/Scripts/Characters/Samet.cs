﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samet : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public bool hasTranslated;
    public GameObject Huxley;
    public GameObject huxleyThreadTrigger;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Father_Huxley _huxleyScript;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = GetComponent<Animator>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
        hasTranslated = false;
    }


    public override void Interact()
    {
        if (hasTranslated && _dialogueValue < 1)
            _dialogueValue++;

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
        if (_dialogueValue == 0 && _huxleyScript.dialogueValue == 3)
        {
            _huxleyScript.ConfirmTaskCompleted();
        }

        if (_dialogueValue == 1)
        {
            huxleyThreadTrigger.SetActive(true);
        }
    }
}
