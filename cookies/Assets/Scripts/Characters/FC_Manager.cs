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
    private bool _looking;
    private Vector3 _magPos;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;
    private Animator _managerAnimator;

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
        _managerAnimator = GetComponent<Animator>();
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

            if (_potentialPornoMag.GetComponent<Tags>() != null)
            {
                _pornoTag = _potentialPornoMag.GetComponent<Tags>();


                for (int i = 0; i < _pornoTag.tags.Length; i++)
                {
                    if (_pornoTag.tags[i] == "Porn")
                    {
                        gameObject.tag = "Untagged";
                        transform.LookAt(_potentialPornoMag.transform);
                        _distracted = true;
                        _managerAnimator.SetBool("distracted", true);
                        _magPos = new Vector3(_potentialPornoMag.transform.position.x, _potentialPornoMag.transform.position.y - 0.05f, _potentialPornoMag.transform.position.z);
                        this.GetComponent<BoxCollider>().enabled = false;

                        break;
                    }
                }
            }

        }
    }

    private void StareAtMagazine()
    {
        gameObject.tag = "Interactable";
        SetDoorToInteractable();

        // change animation
        _looking = true;
        _managerAnimator.SetBool("looking", true);
    }

    public override void InteractAction()
    {
        if (_looking || _drunk)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            keyring.transform.parent = null;
            keyring.GetComponent<Interactable>().InteractAction(); 
            keyring.tag = "Interactable";
        }
        else 
        {
            HandleDialogue(0);
        }
    }

    public void Drink()
    {
        gameObject.tag = "Untagged";
        beerBottle.SetActive(true);
        // change animation
        GetComponent<Animator>().SetBool("drugged", true);
        Destroy(GetComponent<BoxCollider>());
        StartCoroutine(Intoxicate(10.0f));
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

    private IEnumerator Intoxicate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.tag = "Interactable";
        Destroy(GetComponent<Animator>());
       

        SetDoorToInteractable();
        _drunk = true;
        GetComponent<Victim>().Die();
    }

    private void SetDoorToInteractable()
    {
        kitchenDoor.GetComponent<OpenableInteractable>().isLocked = false;
        kitchenDoor.GetComponent<OpenableInteractable>().reqType = RequirementType.Single;
        kitchenDoor.GetComponent<OpenableInteractable>().requiredTags = new string[1];
        kitchenDoor.GetComponent<OpenableInteractable>().requiredTags[0] = "FC Key";
        cashier.GetComponent<Fast_Food_Worker>()._dialogueValue++;
    }

}
