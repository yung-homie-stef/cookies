using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swamp_Hound : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public GameObject cript_walka;
    public GameObject hog;
    public bool hasSpoken;
    public Text noticeText;

    public Brownie_Pan brownieScript;

    public Text hintText;

    private bool _hasHinted = false;

    public int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
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
            requiredTags[0] = "Cosmic Brownies";
            _dialogueValue = 1;

            brownieScript.reqType = RequirementType.Single;
            brownieScript.requiredTags = new string[1];
            brownieScript.requiredTags[0] = "CBD";

            if (!_hasHinted)
            {
                hintText.text += "\n- I've got an oven";
                _hasHinted = true;
            }
        }

        if (_dialogueValue == 2)
        {
            hasSpoken = true;
            if (cript_walka.GetComponent<Cript_Walka>().hasSpoken)
                hog.SetActive(true);
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText("Think you got it twisted mane. These ain't COSMIC BROWNIES.", 6.0f);
    }
}
