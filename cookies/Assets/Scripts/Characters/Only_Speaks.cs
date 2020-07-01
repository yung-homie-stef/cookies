using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlySpeaks : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;

    // Start is called before the first frame update
    public override void InteractAction()
    {

            HandleDialogue(0);
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, false);
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
