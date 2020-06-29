using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randy : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public bool isScared;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;

    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        SetScaredStatus(isScared);
    }

    public override void InteractAction()
    {
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

    void SetScaredStatus(bool status)
    {
        if (status == true)
        {
            GetComponent<Animator>().SetBool("is_scared", true);
        }
    }

    }
