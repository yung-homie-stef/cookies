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
    public GameObject LouisRay;
    public GameObject antlers;
    public GameObject worker;
    public bool isDead = false;
    public GameObject kitchenDoor;
    public GameObject doorLocker;
    private GameObject _contactPoint;

    private bool _startedCharging;
    [SerializeField]
    private Vector3 chargeTarget;
    private float _idleTimer;
    private NavMeshAgent _agent;
    private Louis_Ray _louisRayScript;
    private Fast_Food_Worker _workerScript;

    public AudioClip[] eddieSounds;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        currentState = EddieStates.EddieState_Charge;
        _agent = GetComponent<NavMeshAgent>();
        chargeTarget = target.transform.position;
        _louisRayScript = LouisRay.GetComponent<Louis_Ray>();
        _workerScript = worker.GetComponent<Fast_Food_Worker>();
    }

    public void BeginBattle()
    {
        _animator.enabled = true;
        currentState = EddieStates.EddieState_Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            currentState = EddieStates.EddieState_Dead;
        }

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
            RandomChargeSound();
            _startedCharging = true;
        }

            chargeTimer -= Time.deltaTime;

        if (chargeTimer < 0)
        {
            _animator.SetTrigger("running");
            transform.position += (chargeTarget - transform.position).normalized * 3 * Time.deltaTime;
            chargeTimer = 0;  
        }

        if (Vector3.Distance(transform.position, chargeTarget) < 0.1f)
        {
            _animator.SetBool("done_charging", true);
            
        }
    }
    void DeadState(float deltatime)
    {
        isDead = true;
        if (_louisRayScript.isDead)
        {
            // end thread
            doorLocker.SetActive(false);
            kitchenDoor.GetComponent<OpenableInteractable>().isLocked = false;
            worker.SetActive(true);
            _workerScript._dialogueValue++;
            StartCoroutine(FadeBossMusic.StartFade(Audio_Manager.globalAudioManager.musicSoundArray[2].source, 5.0f, 0.0f));
            this.enabled = false;
            _louisRayScript.enabled = false;
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

    public void EnableAntlers(int param)
    {
        if (param == 1)
        {
            antlers.GetComponent<BoxCollider>().enabled = true;
        }
        else
            antlers.GetComponent<BoxCollider>().enabled = false;
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

    void RandomChargeSound()
    {
        int randomAttackSound = Random.Range(0, eddieSounds.Length);
        GetComponent<AudioSource>().PlayOneShot(eddieSounds[randomAttackSound]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            _contactPoint = other.gameObject;
            TakeDamage("melee", _contactPoint.GetComponent<PlayerDamageRef>().GetPlayerDamage(), _contactPoint.transform.position, _contactPoint.transform.forward);
        }
        else if (other.tag == "ChainsawHBox")
        {
            _contactPoint = other.gameObject;
            TakeDamage("chainsaw", _contactPoint.GetComponent<PlayerDamageRef>().GetPlayerDamage() + 1, _contactPoint.transform.position, _contactPoint.transform.forward);
        }
    }
}
