﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultists : Interactable
{
    public int animationInt;
    public Set_of_Sentences[] sentenceSets;
    public bool isRandom;
    public string[] sentences;
    public GameObject dialogueManager;

    private Animator _animator;
    private string[] currentSentences;
    private Dialogue _dialogue;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("praise_animation", animationInt);
        _dialogue = dialogueManager.GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (!isRandom)
        {
            currentSentences = new string[sentences.Length];
            for (int i = 0; i < currentSentences.Length; i++)
            {
                currentSentences[i] = sentences[i];
            }
            _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
        }
        else
        {
            HandleDialogue(Random.Range(0,3));
        }
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
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
}