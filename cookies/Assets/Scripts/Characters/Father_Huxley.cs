using System.Collections;
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
    public GameObject confessionalWindow;
    public GameObject koolAid;
    public GameObject heartKey;
    public Text noticeText;

    public Text hintText;

    [SerializeField]
    public int dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Inventory _inventory;
    private bool _requiresPayment = false;
    private Notice _notice;
    private bool eventHappensWhenTalkingIsDone;
    private bool needsBomb = false;
    private string failMessage;

    private bool _hasHinted = false;

    // Start is called before the first frame update
    new void Start()
    {
        _requiresPayment = false;
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        dialogueValue = 0;
        _animator = GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _animator.SetInteger("praise_animation", animationInt);
        eventHappensWhenTalkingIsDone = true;
    }

    private void Update()
    {
        if (_animator.enabled == false)
        {
            heartKey.SetActive(true);
            heartKey.GetComponent<Interactable>().InteractAction();
            enabled = false;
        }
    }


    public override void InteractAction()
    {
        if (_requiresPayment)
        {
            ConfirmTaskCompleted();
            HandleDialogue(dialogueValue);
            _requiresPayment = false;
        }
        else if (needsBomb)
        {
            ConfirmTaskCompleted();
            HandleDialogue(dialogueValue);
            needsBomb = false;
        }
        else
            HandleDialogue(dialogueValue);
    }

    public void ConfirmTaskCompleted()
    {
        dialogueValue++;
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
        if (dialogueValue == 0)
        {
            _requiresPayment = true;
            failMessage = "You must first make an offering. Any form of currency will do.";
            ChangeReqs("Currency");

            if (!_hasHinted)
            {
                hintText.text += "\n- $$$$$$";
            }
        }

        if (dialogueValue == 1)
        {
            confessionDoor.GetComponent<OpenableInteractable>().isLocked = false;
            confessionalWindow.GetComponent<BoxCollider>().enabled = true;
            _hasHinted = false;
        }

        if (dialogueValue == 2)
        {
            needsBomb = true;
            failMessage = "Sorry brother, but this won't do. I am in need of a PIPE BOMB.";
            ChangeReqs("Pipe Bomb");
            hintText.text += "\n- Look for LEAD PIPE";
            hintText.text += "\n- Overzealous fourth floor tenant";
        }

        if (dialogueValue == 3)
        {
            koolAid.SetActive(true);
        }

        if (dialogueValue == 4)
        {
            hintText.text += "\n- Lace the drink";
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText(failMessage, 6.0f);
    }

    private void ChangeReqs(string req)
    {
        reqType = RequirementType.Single;
        requiredTags = new string[1];
        requiredTags[0] = req;
    }
}
