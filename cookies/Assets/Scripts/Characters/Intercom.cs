using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercom : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject door;
    public GameObject intercomCamera;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;
    private OpenableInteractable _openable;
    private bool _turnedOn = false;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _openable = door.GetComponent<OpenableInteractable>();
    }

    public override void InteractAction()
    {
        if (!_turnedOn)
        {
            intercomCamera.SetActive(true);
            _turnedOn = true;
        }
        else 
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


    public override void ConversationEndEvent()
    {
        intercomCamera.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        _openable.SetOpenToggle(1);
        _openable.SetCloseToggle(0);
        _openable.isLocked = false;
    }
}
