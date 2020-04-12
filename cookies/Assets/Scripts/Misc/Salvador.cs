using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvador : Interactable
{
    public GameObject dialogueManager;
    public GameObject gun;
    public GameObject ratPrimacy;
    public Light livingRoomLight;
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

                    if (_dialogueValue < 5) // so that they cant access the ritual by feeding him MORE food
                    {
                        _dialogueValue++;
                    }
                    break;
                }
                else if (_tags.tags[i] == "Poison")
                {
                    GetComponent<Victim>().Die(); // kill salvador if he is fed poison
                }
            }    
        }
    }

    public override void Interact()
    {
        //TODO: look to scriptable objects to avoid looking up harcoded

        switch (_dialogueValue)
        {
            case 0: // initial convo
                currentSentences = new string[4];
                for (int i =0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i];
                }
                _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
                break;

            case 1: // after feeding first item
                currentSentences = new string[3];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i+4];
                }
                _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
                break;

            case 2: // after feeding three items
                currentSentences = new string[5];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i + 7];
                }
                _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
                break;

            case 5: // where he gives the glock
                currentSentences = new string[7];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i + 12];
                }
                _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
                gun.SetActive(true);
                break;

            case 6: // after killing janitor
                currentSentences = new string[6];
                for (int i = 0; i < currentSentences.Length; i++)
                {
                    currentSentences[i] = sentences[i + 19];
                }
                _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
                break;

            default:
                break;

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

    public void StartCeremony()
    {
        if (_dialogueValue == 5)
        {
            ratPrimacy.SetActive(true);
            _dialogueValue++;
            livingRoomLight.color = new Color32(171, 38, 31, 255); // change light to demonic red
        }
    }
}
