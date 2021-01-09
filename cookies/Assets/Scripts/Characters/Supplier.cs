using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplier : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;

    public void BeginPhoneTrigger()
    {
        StartCoroutine(CallDealer(10.0f));
    }

    private IEnumerator CallDealer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        InteractAction();
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
}
