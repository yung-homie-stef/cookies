using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Custodian : Interactable
{
    public float startWaitTime;
    public string[] sentences;
    public Transform[] moveSpots;
    public GameObject mop;
    public GameObject dialogueManager;
    public GameObject Salvador;

    private int _randomSpot;
    private float _waitTime;
    private bool _hasSpoken;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Dialogue _dialogue;
    private Salvador _salvadorScript;

    // Start is called before the first frame update
    void Start()
    {
        _waitTime = startWaitTime;
        _randomSpot = Random.Range(0, moveSpots.Length);
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _hasSpoken = false;
        _salvadorScript = Salvador.GetComponent<Salvador>();
    }

    // Update is called once per frame
    void Update()
    {
        // move to a random spot
        _agent.destination = moveSpots[_randomSpot].position;

        // if distance between custodian and current point is less than 0.2
        // 
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        { 
            if (_waitTime <= 0)
            {
                mop.SetActive(false);
                _animator.SetBool("isSweeping", false);
                _randomSpot = Random.Range(0, moveSpots.Length);
                _waitTime = startWaitTime;
            }
            else
            {
                mop.SetActive(true);
                _animator.SetBool("isSweeping", true);
                _waitTime -= Time.deltaTime;
            }
         }

        if (_animator.enabled == false)
        {
            mop.transform.parent = null;
            mop.GetComponent<Rigidbody>().isKinematic = false;
            _salvadorScript.StartCeremony();
        }
    }

    public override void Interact()
    {
        if (_hasSpoken == false)
        {
            _dialogue.index = 0;
            _dialogue._canAdvance = true;
            dialogueManager.SetActive(true);
            UpdateDialogue(sentences);
            _hasSpoken = true;
        }
    }

    private void UpdateDialogue(string[] lines)
    {
        _dialogue.sentences = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            _dialogue.sentences[i] = lines[i];
        }
    }
}
