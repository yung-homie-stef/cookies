using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secretary : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject shojiDoor;
    public GameObject blackout;
    public GameObject blackoutCanvas;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
    }

    public override void InteractAction()
    {
        HandleDialogue(0);
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
        if (_dialogueValue == 0)
        {
            _dialogueValue++;
            shojiDoor.GetComponent<OpenableInteractable>().isLocked = false;
            blackoutCanvas.SetActive(true);
            blackout.GetComponent<Animator>().SetBool("faded", true);
            blackout.GetComponent<Animator>().SetBool("faded", false);
        }
    }

}
