using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eddie : Victim
{
    public enum EddieStates
    {
        EddieState_Idle,
        EddieState_Charge,
        EddieState_Dead
    }

    public EddieStates currentState;
    public float chargeTimer = 1.5f;
    public Transform target;
    public float wanderTimer;
    public float wanderRadius;

    private bool _startedCharging;
    [SerializeField]
    private Vector3 chargeTarget;
    private float _idleTimer;
    private NavMeshAgent _agent;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        currentState = EddieStates.EddieState_Charge;
        _agent = GetComponent<NavMeshAgent>();
        chargeTarget = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EddieStates.EddieState_Idle:
            {
                 IdleState(Time.deltaTime);
                 break;
            }
            case EddieStates.EddieState_Charge:
            {
                 ChargeState(Time.deltaTime);
                 break;
            }
            case EddieStates.EddieState_Dead:
            {
                 DeadState(Time.deltaTime);
                 break;
            }
        }
    }

    void IdleState(float deltatime)
    {
        transform.LookAt(target);

        _idleTimer -= Time.deltaTime;

        if (_idleTimer < 0)
        {
            _animator.SetTrigger("charging");
            currentState = EddieStates.EddieState_Charge;
        }
    }
    void ChargeState(float deltatime)
    {

        if (!_startedCharging)
        {
            chargeTarget = target.transform.position;
            _animator.SetTrigger("charging");
            _startedCharging = true;
        }


            chargeTimer -= Time.deltaTime;

        if (chargeTimer < 0)
        {
            _animator.SetTrigger("running");
            transform.position += (chargeTarget - transform.position).normalized * 2 * Time.deltaTime;
            chargeTimer = 0;  
        }

        if (Vector3.Distance(transform.position, chargeTarget) < 0.1f)
        {
            _animator.SetBool("done_charging", true);
            
        }

    }
    void DeadState(float deltatime)
    {

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }


    public void ResetCharge()
    {
        _animator.ResetTrigger("charging");
        _animator.ResetTrigger("running");
        _animator.SetBool("done_charging", false);
        currentState = EddieStates.EddieState_Idle;
        chargeTimer = 1.5f;
        _idleTimer = 2.0f;
        
        _startedCharging = false;
    }
}
