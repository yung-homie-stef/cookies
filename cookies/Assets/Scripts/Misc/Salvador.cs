using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvador : Interactable
{
    public GameObject dialogueManager;
    public GameObject gun;
    public GameObject ratPrimacy;
    public Light livingRoomLight;
    public Set_of_Sentences[] sentenceSets;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Fullness _salvadorsBelly = 0;

    private enum Fullness
    {
        empty = 0,
        fed_once = 1,
        fed_twice = 2,
        fed_thrice = 3,
        fed_fourfold = 4,
        full = 5
    }

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

                    if ((int)_salvadorsBelly < 5) // so that they cant access the ritual by feeding him MORE food
                    {
                        _salvadorsBelly++;
                        Debug.Log(_salvadorsBelly);
                    }
                    _dialogueValue = (int)_salvadorsBelly;
                    Interact();

                    if ((int)_salvadorsBelly == 5)
                    {
                        gun.SetActive(true); // give player gun when he is full
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
        HandleDialogue(_dialogueValue);
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
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
