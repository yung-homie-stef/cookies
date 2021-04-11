using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class El_Cuco : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public bool isStanding;
    public bool isFighting;

    public Text hintText;

    private bool _hasHinted = false;

    public GameObject pigRoomDoor;
    public GameObject criptWalka;
    public GameObject swampHound;
    public GameObject cop;

    [SerializeField]
    private int _dialogueValue = 0;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("standing", isStanding);
        _animator.SetBool("combat", isFighting);
    }

    public override void InteractAction()
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

    public override void ConversationEndEvent()
    {
        if (_dialogueValue == 0)
        {
            // open door to hog killing room
            pigRoomDoor.GetComponent<OpenableInteractable>().isLocked = false;
            cop.SetActive(false);

            if (!_hasHinted)
            {
                hintText.text += "\n- 211";
            }
        }
        else if (_dialogueValue == 1)
        {
            // change cript walka and swamp hounds dialogues
            criptWalka.GetComponent<Cript_Walka>()._dialogueValue++;
            swampHound.GetComponent<Swamp_Hound>()._dialogueValue++;

            if (!_hasHinted)
            {
                hintText.text += "\n- Look out for your dawgs";
            }
        }
    }
}
