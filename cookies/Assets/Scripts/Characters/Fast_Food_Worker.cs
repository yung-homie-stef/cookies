using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fast_Food_Worker : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public Text noticeText;
    public bool threadAvailable;
    public End_Condition crown_fried_Thread;
    public GameObject blackOut;
    public GameObject bottle;

    [SerializeField]
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
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        if (threadAvailable)
        {
            if (_dialogueValue == 0)
            {
                _dialogueValue++;
                HandleDialogue(_dialogueValue);
            }
            else
                HandleDialogue(_dialogueValue);
        }
        else
            HandleDialogue(_dialogueValue);
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, eventHappensWhenTalkingIsDone);
    }

    public override void ConversationEndEvent()
    {
        if (_dialogueValue == 3)
        {
            StartCoroutine(CompleteChickenThread(5.0f));
            blackOut.GetComponent<Animator>().SetBool("faded", true);
        }

        if (_dialogueValue == 1)
        {
            bottle.tag = "Interactable";
        }
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


    private IEnumerator CompleteChickenThread(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(crown_fried_Thread);

    }

}
