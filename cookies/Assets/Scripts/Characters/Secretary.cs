using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Secretary : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject shojiDoor;
    public GameObject blackout;
    public GameObject blackoutCanvas;
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
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        if (_dialogueValue == 0 || _dialogueValue == 2)
        {
            HandleDialogue(_dialogueValue);
        }
        else if (_dialogueValue == 1)
        {
            HandleDialogue(_dialogueValue);
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
            _dialogueValue++;
            reqType = RequirementType.Single;
            requiredTags = new string[1];
            requiredTags[0] = "Card";

        }
        else if (_dialogueValue == 1)
        {
            
            shojiDoor.GetComponent<OpenableInteractable>().isLocked = false;
            blackoutCanvas.SetActive(true);
            blackout.GetComponent<Animator>().SetBool("faded", true);
            StartCoroutine(UnfadeBlack(1.5f));
            _dialogueValue++;
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText("I'm sorry, I can't help you with this. Would you like to see a brochure?");
    }

    private IEnumerator UnfadeBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        blackout.GetComponent<Animator>().SetBool("faded", false);
    }

}
