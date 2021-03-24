using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull_Thug : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject dialogueManager;
    public GameObject criptWalka;
    public GameObject swampHound;
    public GameObject criptsGun;
    public GameObject pigRoomSquad;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Rigidbody _rigidbody;
    private Cript_Walka _criptScript;
    private Swamp_Hound _swampScript;

    protected Rigidbody[] childrenBody;

    // Start is called before the first frame update
    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = true;
        _animator = gameObject.GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
        childrenBody = GetComponentsInChildren<Rigidbody>();

        _criptScript = criptWalka.GetComponent<Cript_Walka>();
        _swampScript = swampHound.GetComponent<Swamp_Hound>();
    }

    public override void InteractAction()
    {
        HandleDialogue(0);

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

    public override void ConversationEndEvent()
    {
        KillSkull();
    }

    private void KillSkull()
    {

        _animator.enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        foreach (var body in childrenBody)
        {
            body.isKinematic = false;
            body.AddForceAtPosition(transform.forward * - 300.0f, transform.position);
        }

       criptWalka.GetComponent<Animator>().SetBool("cocked", true);
       criptsGun.SetActive(true);

       _criptScript._dialogueValue++;
       _swampScript._dialogueValue++;

        pigRoomSquad.SetActive(false);
    }
}
