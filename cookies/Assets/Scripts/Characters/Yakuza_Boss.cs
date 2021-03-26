using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yakuza_Boss : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject deadSoviets;
    public GameObject livingSoviets;
    public GameObject sovietDoor;
    public End_Condition yakuza_Thread;
    public GameObject blackOut;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private OpenableInteractable _sovietOpenable;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _sovietOpenable = sovietDoor.GetComponent<OpenableInteractable>();
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
            deadSoviets.SetActive(true);
            livingSoviets.SetActive(false);
            _dialogueValue++;
        }

        if (_dialogueValue == 2)
        {
            blackOut.GetComponent<Animator>().SetBool("faded", true);
            StartCoroutine(CompleteYakuzaThread(5.0f));
        }
    }

    private IEnumerator CompleteYakuzaThread(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(yakuza_Thread);

    }

    public void IchiDead()
    {
        _dialogueValue = 2;
    }

}
