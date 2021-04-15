using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Butch : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;

    public Text hintsText;

    private bool _hasHinted = false;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;
    public GameObject ringing;


    public override void InteractAction()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        HandleDialogue(0);

        if (ringing != null)
        {
            Destroy(ringing);
        }
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, true);
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
        if (!_hasHinted)
        {
            hintsText.text += "\n- Lobby";
            _hasHinted = true;
        }
    }

}