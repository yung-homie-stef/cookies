﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Father_Huxley : Interactable
{
    public int animationInt;
    public GameObject dialogueManager;
    public GameObject confessionDoor;
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public Text noticeText;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Inventory _inventory;
    private bool _requiresPayment;
    private Notice _notice;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _requiresPayment = false;
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
        _animator = GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _animator.SetInteger("praise_animation", animationInt);
        eventHappensWhenTalkingIsDone = true;
    }

    public override void Interact()
    {
        if (_requiresPayment) // when huxley asks for tithes
        {
            if (CheckForItem("Currency") == false) // check for tithes
            {
                _notice.ChangeText("TITHES REQUIRED"); // if you don't have money remind the player they need money
            }
            else
            {
                ConfirmTaskCompleted(); // otherwise pay him and progress the story
                _requiresPayment = false;
            }
        }

        if (_dialogueValue == 2)
        {
            if (CheckForItem("Pipe_Bomb") == false) // check for tithes
            {
                _notice.ChangeText("PIPE BOMB REQUIRED"); // if you don't have money remind the player they need money
            }
            else
            {
                ConfirmTaskCompleted(); // otherwise pay him and progress the story
            }
        }

        {
            HandleDialogue(_dialogueValue);
        }
    }

    public void ConfirmTaskCompleted()
    {
        _dialogueValue++;
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

    private bool CheckForItem(string tag)
    {

        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == tag)
                    {
                        _inventory.isSlotFull[i] = false;
                        Destroy(_inventory.playerInventoryItems[i]);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public override void ConversationEndEvent()
    {
        if (_dialogueValue == 0)
        {
            _requiresPayment = true;
        }
        else if (_dialogueValue == 1)
        {
            confessionDoor.tag = "Interactable";
        }
    }
}
