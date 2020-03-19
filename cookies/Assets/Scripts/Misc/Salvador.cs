using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvador : Interactable
{
    public GameObject dialogueManager;
    public string[] sentences;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;

    // Start is called before the first frame update
    void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
    }


    private void OnTriggerEnter(Collider other)
    {

        // everytime you feed salvador delete the food item and increase the dialogue value so he can speak after being fed several times
        if (other.tag == "Interactable")
        {
            _tags = other.GetComponent<Tags>();

            for (int i = 0; i < _tags.tags.Length; i++)
            {
                if (_tags.tags[i] == "Edible")
                {
                    Destroy(other.gameObject);
                    _dialogue.index = 0;
                    _dialogue._canAdvance = true;
                    _dialogueValue++;
                    break;
                }
            }    
        }
    }

    public override void Interact()
    {
        dialogueManager.SetActive(true);
        switch (_dialogueValue)
        {
            case 0:
                currentSentences = new string[4];
                for (int i =0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i];
                }
                UpdateDialogue(currentSentences);
                break;

            case 1:
                currentSentences = new string[3];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i+4];
                }
                UpdateDialogue(currentSentences);
                break;

            case 2:
                currentSentences = new string[5];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i + 7];
                }
                UpdateDialogue(currentSentences);
                break;

        }
        
    }

    private void UpdateDialogue(string[] lines)
    {
        _dialogue.sentences = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            _dialogue.sentences[i] = lines[i];
        }
    }
}
