using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Only_Speaks : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;

    public bool doesHint;
    private bool _hasHinted = false;
    public string hint;

    public Text hintText;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;

    // Start is called before the first frame update
    public override void InteractAction()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        HandleDialogue(0);
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
        if (doesHint)
        {
            if (!_hasHinted)
            {
                hintText.text += "\n" + hint;
                _hasHinted = true;
            }
        }
    }
}
