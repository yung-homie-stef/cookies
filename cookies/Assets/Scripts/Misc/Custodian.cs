using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Custodian : MonoBehaviour
{
    public float startWaitTime;
    public Transform[] moveSpots;
    public GameObject mop;

    private int _randomSpot;
    private float _waitTime;
    private NavMeshAgent _agent;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _waitTime = startWaitTime;
        _randomSpot = Random.Range(0, moveSpots.Length);
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
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
    }
}
