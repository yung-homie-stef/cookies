using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samet : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public bool hasTranslator;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = GetComponent<Animator>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = false;
        _dialogueValue = 0;
        hasTranslator = false;
    }


    public override void Interact()
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
}
