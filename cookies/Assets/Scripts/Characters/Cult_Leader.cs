using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cult_Leader : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;

    [SerializeField]
    private string[] currentSentences;
    [SerializeField]
    private int _dialogueValue;
    private Dialogue _dialogue;

    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
    }

    // Start is called before the first frame update
    public override void InteractAction()
    {
        HandleDialogue(_dialogueValue);
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
        if (_dialogueValue == 0)
        {
            _dialogueValue++;
            reqType = RequirementType.Single;
            requiredTags = new string[1];
            requiredTags[0] = "DMT";
        }
    }
}
