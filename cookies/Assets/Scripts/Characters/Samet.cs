using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samet : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public bool hasTranslated;
    public GameObject Huxley;
    public GameObject huxleyThreadTrigger;
    public GameObject galaxyExit;
    public GameObject congregation;
    public GameObject massSuicide;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Father_Huxley _huxleyScript;
    Galaxy_Exit _galaxyExitScript;

    // Start is called before the first frame update
    new void Start()
    {
        _galaxyExitScript = galaxyExit.GetComponent<Galaxy_Exit>();
        _huxleyScript = Huxley.GetComponent<Father_Huxley>();
        _animator = GetComponent<Animator>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
        hasTranslated = false;
    }


    public override void InteractAction()
    {
        if (hasTranslated && _dialogueValue < 1)
            _dialogueValue++;

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
        if (_dialogueValue == 0 && _huxleyScript.dialogueValue == 3)
        {
            _huxleyScript.ConfirmTaskCompleted();
        }

        if (_dialogueValue == 1)
        {
            huxleyThreadTrigger.SetActive(true);
            _galaxyExitScript.huxleyThreadComplete = true;
            congregation.SetActive(false);
            massSuicide.SetActive(true);
            
        }
    }
}
