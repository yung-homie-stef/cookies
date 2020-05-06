using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salvador : Interactable
{
    public GameObject dialogueManager;
    public GameObject gun;
    public GameObject blackOut;
    public GameObject ratPrimacy;
    public Light livingRoomLight;
    public Set_of_Sentences[] sentenceSets;
    public End_Condition son_of_sal_Thread;
    public AudioClip eatSound;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Fullness _salvadorsBelly = 0;
    private bool eventHappensWhenTalkingIsDone;

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
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
        eventHappensWhenTalkingIsDone = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Game_Manager.globalGameManager.EndGame(son_of_sal_Thread);
        }
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
                    _dialogue.dialogueIndex = 0;
                    _dialogue._canAdvance = true;

                    if ((int)_salvadorsBelly < 5) // so that they cant access the ritual by feeding him MORE food
                    {
                        _salvadorsBelly++;
                        GetComponent<AudioSource>().PlayOneShot(eatSound);
                        Audio_Manager.globalAudioManager.PlaySound("ping", Audio_Manager.globalAudioManager.intangibleSoundArray);
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
                    //_dialogueValue = 8;
                    //Interact();
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

    public void StartCeremony()
    {
        if (_dialogueValue == 5)
        {
            ratPrimacy.SetActive(true);
            _dialogueValue++;
            livingRoomLight.color = new Color32(171, 38, 31, 255); // change light to demonic red
            eventHappensWhenTalkingIsDone = true;
        }
    }

    public override void ConversationEndEvent()
    {
        StartCoroutine(CompleteSalvadorsThread(5.0f));
        blackOut.GetComponent<Animator>().SetBool("faded", true);
    }

    private IEnumerator CompleteSalvadorsThread(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(son_of_sal_Thread);
    }

}
