using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_Manager : Interactable
{
    private Tags _pornoTag;
    private GameObject _potentialPornoMag;
    private bool _distracted = false;
    private bool _drunk = false;
    private Vector3 _magPos;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;
    private OpenableInteractable _kitchenDoorScript;

    public float speed = 0.25f;
    public GameObject keyring;
    public GameObject beerBottle;
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public Text noticeText;
    public GameObject cashier;
    public GameObject kitchenDoor;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _notice = noticeText.GetComponent<Notice>();
        _kitchenDoorScript = kitchenDoor.GetComponent<OpenableInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_distracted)
        {
            float _walkSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _magPos, _walkSpeed);
        }

        if (Vector3.Distance(transform.position,_magPos) < 0.001f)
        {
            StareAtMagazine();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            _potentialPornoMag = other.gameObject;
            _pornoTag = _potentialPornoMag.GetComponent<Tags>();

            for (int i =0; i < _pornoTag.tags.Length; i++)
            {
                if (_pornoTag.tags[i] == "Porn")
                {
                    gameObject.tag = "Untagged";
                    _distracted = true;
                    _magPos = new Vector3(_potentialPornoMag.transform.position.x, _potentialPornoMag.transform.position.y + 0.425f, _potentialPornoMag.transform.position.z);

                    break;
                }
            }

        }
    }

    private void StareAtMagazine()
    {
        gameObject.tag = "Interactable";
        _kitchenDoorScript.isLocked = false;
        _kitchenDoorScript.reqType = RequirementType.Single;
        _kitchenDoorScript.requiredTags = new string[1];
        _kitchenDoorScript.requiredTags[0] = "FC Key";
        this.enabled = false;

        // change animation
    }

    public override void InteractAction()
    {
        if (_distracted || _drunk)
        {  
            keyring.transform.parent = null;
            keyring.GetComponent<Interactable>().InteractAction(); 
            keyring.tag = "Interactable";
            cashier.GetComponent<Fast_Food_Worker>()._dialogueValue++;
            gameObject.tag = "Untagged";
        }
        else 
        {
            HandleDialogue(0);
        }
    }

    public void Drink()
    {
        beerBottle.SetActive(true);
        _drunk = true;
        _kitchenDoorScript.isLocked = false;
        _kitchenDoorScript.reqType = RequirementType.Single;
        _kitchenDoorScript.requiredTags = new string[1];
        _kitchenDoorScript.requiredTags[0] = "FC Key";
        // change animation
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
