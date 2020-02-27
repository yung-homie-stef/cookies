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

    // Start is called before the first frame update
    void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // everytime you feed salvador delete the food item and increase the dialogue value so he can speak after being fed several times
        if (other.tag == "Interactable")
        {
            Destroy(other.gameObject);
            _dialogue.index = 0;
            _dialogue._canAdvance = true;
            _dialogueValue++;
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

            //case 5:
            //    currentSentences = new string[5];
            //    for (int i = 0; i < currentSentences.Length; i++)
            //    {
            //        currentSentences[i] = sentences[i + 7];
            //    }
            //    UpdateDialogue(currentSentences);
            //    break;
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
