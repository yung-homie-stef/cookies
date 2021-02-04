using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_Manager : Interactable
{
    private Tags _pornoTag;
    private GameObject _potentialPornoMag;
    private bool _distracted = false;
    private Vector3 _magPos;

    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;

    public float speed = 0.25f;
    public GameObject keyring;
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public Text noticeText;
    public GameObject cashier;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _dialogueValue = 0;
        _notice = noticeText.GetComponent<Notice>();
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
        // change animation
        // make door require a key, but also unlock it
    }

    public override void InteractAction()
    {
        if (_distracted)
        {  
            keyring.transform.parent = null;
            keyring.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
            keyring.tag = "Interactable";
            cashier.GetComponent<Fast_Food_Worker>()._dialogueValue++;
        }
        else
        {

        }
    }
}
