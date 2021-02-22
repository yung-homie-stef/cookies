using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Norman : Victim
{
   public enum NormanStates
    {
        NormanState_Idle,
        NormanState_Walk,
        NormanState_Shoot,
        NormanState_Melee,
        NormanState_Smoke,
        NormanState_Crawl
    }

    public NormanStates currentState = NormanStates.NormanState_Idle;

    public Transform target = null;
    public float wanderRadius;
    public float wanderTimer;
    public float meleeRange = 1.0f;

    [SerializeField]
    private float _bossDistance;
    [SerializeField]
    private float _actionChangeTimer;
    private int shootNumber = 0;
    private NavMeshAgent _agent;
    private float timer;
    private bool _hasCrouched;
    

    new void Start()
    {
        base.Start();
        currentState = NormanStates.NormanState_Crawl;
        _agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public void BeginBattle()
    {
        
    }

    private void Update()
    {
        switch (currentState)
        {
            case NormanStates.NormanState_Idle:
            {
                IdleState(Time.deltaTime);
                break;
            }
            case NormanStates.NormanState_Walk:
            {
                WalkState(Time.deltaTime);
                break;
            }
            case NormanStates.NormanState_Shoot:
            {
                ShootState(Time.deltaTime);
                break;
            }
            case NormanStates.NormanState_Melee:
            {
                MeleeState(Time.deltaTime);
                break;
            }
            case NormanStates.NormanState_Smoke:
            {
                SmokeState(Time.deltaTime);
                break;
            }
            case NormanStates.NormanState_Crawl:
            {
                CrawlState(Time.deltaTime);
                break;
            }
        }

        if (target != null)
        {
            _bossDistance = Vector3.Distance(transform.position, target.transform.position);
        }

    }

    void IdleState(float time)
    {

    }
    void WalkState(float time)
    {
        timer += Time.deltaTime;
        _agent.speed = 1f;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            timer = 0;
        }

        if (_bossDistance <= meleeRange)
        {
            currentState = NormanStates.NormanState_Melee;
        }
    }
    void ShootState(float time)
    {

    }
    void MeleeState(float time)
    {
        _animator.SetTrigger("melee");
        _agent.Stop();
        transform.LookAt(target);
    }
    void SmokeState(float time)
    {
        _animator.SetTrigger("smoking");
    }
    void CrawlState(float time)
    {
        if (!_hasCrouched)
        {
            _animator.SetBool("prone", true);
            _hasCrouched = true;
            _actionChangeTimer = ChangeActionTimer(10.0f, 14.0f);
        }

        _actionChangeTimer -= Time.deltaTime;
        timer += Time.deltaTime;
        _agent.speed = 0.75f;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            timer = 0;
        }

        if (_actionChangeTimer <= 0)
        {
            _actionChangeTimer = 0;
            _animator.SetBool("prone", false);
            currentState = NormanStates.NormanState_Walk;
        }
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private float ChangeActionTimer(float min, float max)
    {
        return Random.Range(min, max);
    }

}
