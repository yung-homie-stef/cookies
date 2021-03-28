using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Custodian : Interactable 
{
    public GameObject dialogueManager;
    public GameObject mop;
    public GameObject Salvador;
    public GameObject keyring;
    public string[] sentences;
    public AudioClip sweepSFX;
    public BoxCollider _custodianBoxCollider;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private int _randomSpot;
    private float _waitTime;
    private NavMeshAgent _agent;
    private Salvador _salvadorScript;
    private bool eventHappensWhenTalkingIsDone;

    private float timer = 0.0f;
    private float wanderRadius = 10.0f;
    public float wanderTimer = 3.0f;
    private bool _dead = false;


    // Start is called before the first frame update
    new void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _salvadorScript = Salvador.GetComponent<Salvador>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = false;
        _custodianBoxCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_animator.enabled == true)
        {
            if (_agent.gameObject.transform.position == _agent.destination)
            {
                _animator.SetBool("isSweeping", true);
            }

            if (timer >= wanderTimer) // change position
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _agent.SetDestination(newPos);
                _animator.SetBool("isSweeping", false);

                timer = 0;
            }
        }

        if (_animator.enabled == false)
        {
            if (!_dead)
            {
                _salvadorScript.StartCeremony();
                _salvadorScript._sequenceNumber++;
                _agent = null;
                _custodianBoxCollider.enabled = false;
                StartCoroutine(GivePlayerKeys(1.5f));
                _dead = true;
            }
        }
    }

    public override void InteractAction()
    {
        currentSentences = new string[2];
        for (int i = 0; i < currentSentences.Length; i++)
        {
            currentSentences[i] = sentences[i];
        }

        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, eventHappensWhenTalkingIsDone);
    }

    private IEnumerator GivePlayerKeys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        keyring.transform.parent = null;
        keyring.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
        keyring.tag = "Interactable";
    }

    public void Sweep()
    {
        //GetComponent<AudioSource>().PlayOneShot(sweepSFX);
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

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
