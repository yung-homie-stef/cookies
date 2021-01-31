using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cript_Walka : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public GameObject swamp_hound;
    public GameObject hog;
    public bool hasSpoken;
    public Text noticeText;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
        hasSpoken = false;
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        if (_dialogueValue == 0)
        {
            HandleDialogue(0);
        }
        else if (_dialogueValue == 1)
        {
            _dialogueValue = 2;
        }
        else if (_dialogueValue == 2)
        {
            HandleDialogue(1);
        }

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
            reqType = RequirementType.Single;
            requiredTags = new string[1];
            requiredTags[0] = "Cookies";
            _dialogueValue = 1;
        }

        if (_dialogueValue == 2)
        {
            hasSpoken = true;
            if (swamp_hound.GetComponent<Swamp_Hound>().hasSpoken)
                hog.SetActive(true);
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText("Looks like you got my order mixed up. I wanted a bag of them COOKIES.");
    }
}
