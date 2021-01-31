using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Text noticeText;

    [SerializeField]
    private int _dialogueValue;

    [SerializeField]
    private int _sequenceNumber;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;


    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
        eventHappensWhenTalkingIsDone = true;
        _sequenceNumber = 0;
        _notice = noticeText.GetComponent<Notice>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CompleteSalvadorsThread(1.0f);
        }
    }

   
    public override void InteractAction()
    {
        if (_sequenceNumber == 0)
        {
            HandleDialogue(_dialogueValue); // talk to salvador
        }
        else if (_sequenceNumber == 1) // give him first meal
        {
            HandleDialogue(_dialogueValue);
        }
        else if (_sequenceNumber == 2) // give him second meal
        {
            StartCoroutine(EmptyStomach(0.5f));
            _sequenceNumber++;
           
        }
        else if (_sequenceNumber == 3) // give him third meal
        {
            HandleDialogue(_dialogueValue);
        }
        else if (_sequenceNumber == 4)
        {
            HandleDialogue(_dialogueValue);
        }
        else if (_sequenceNumber == 5)
        {
            HandleDialogue(_dialogueValue);
        }
        else if (_sequenceNumber == 6)
        {
            HandleDialogue(_dialogueValue);
        }


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
        if (_sequenceNumber == 0 || _sequenceNumber == 1 || _sequenceNumber == 2 || _sequenceNumber == 3 ||
            _sequenceNumber == 4 || _sequenceNumber == 5)
        {
            CommandPlayerToGetMore();
        }
        else if ()
        


        //StartCoroutine(CompleteSalvadorsThread(5.0f));
        //blackOut.GetComponent<Animator>().SetBool("faded", true);
    }

    private IEnumerator CompleteSalvadorsThread(float waitTime)
    {
        Debug.Log("fuck u moron");
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(son_of_sal_Thread);
        
    }

    private IEnumerator EmptyStomach(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        reqType = RequirementType.Single;

    }

    private void CommandPlayerToGetMore()
    {
        _dialogueValue++;
        _sequenceNumber++;
        reqType = RequirementType.Single;
        requiredTags = new string[1];
        requiredTags[0] = "Edible";
    }

    public override void FailMessage()
    {
        _notice.ChangeText("Inconceivable squire... My ilk and I cannot possibly consume this rubbish!");
    }

}
