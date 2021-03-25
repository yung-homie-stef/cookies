﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Custodian : Interactable 
{
    public GameObject dialogueManager;
    public float startWaitTime;
    public Transform[] moveSpots;
    public GameObject mop;
    public GameObject Salvador;
    public GameObject keyring;
    public string[] sentences;
    public AudioClip sweepSFX;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private int _randomSpot;
    private float _waitTime;
    private NavMeshAgent _agent;
    private Salvador _salvadorScript;
    private bool eventHappensWhenTalkingIsDone;

    // Start is called before the first frame update
    new void Start()
    {
        _waitTime = startWaitTime;
        _randomSpot = Random.Range(0, moveSpots.Length);
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _salvadorScript = Salvador.GetComponent<Salvador>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        eventHappensWhenTalkingIsDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent)
            return;

        // move to a random spot
        _agent.destination = moveSpots[_randomSpot].position;

        // if distance between custodian and current point is less than 0.2

        if (_animator.enabled == false)
        {
            _salvadorScript.StartCeremony();
            _salvadorScript._sequenceNumber++;
            _agent = null;
            StartCoroutine(GivePlayerKeys(1.5f));
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
}
