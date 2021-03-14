using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cult_Leader : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public Swamp_Boat boat;
    public GameObject boatCollision;
    public GameObject dancers;

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
        if (_dialogueValue == 0)
        {
            HandleDialogue(0);
        }
        else if (_dialogueValue == 1)
        {
            _dialogueValue = 2;
            HandleDialogue(1);
        }
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
            _dialogueValue = 1;
        }
        else if (_dialogueValue == 2)
        {
           dancers.SetActive(true);
           boatCollision.SetActive(true);
           boat.reqType = RequirementType.Single;
           boat.requiredTags = new string[1];
           boat.requiredTags[0] = "Gas";
           boat.tag = "Interactable";
        }
    }
}
