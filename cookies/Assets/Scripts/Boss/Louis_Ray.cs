using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Louis_Ray : Victim
{
    public enum LouisRayStates
    {
        LouisRay_Idle,
        LouisRay_Walk,
        LouisRay_Slash,
        LouisRay_Spin,
        LouisRay_Dead
    }

    public LouisRayStates currentState;

    public Transform target = null;
    public float attackRange = 1.0f;
    public float movementSpeed;
    public bool isDead = false;
    public float spinRadius = 1.0f;
    private float timer;
    public float wanderTimer;
    public GameObject Eddie;
    public GameObject worker;
    public GameObject kitchenDoor;
    public GameObject doorLocker;
    private GameObject _contactPoint;

    public GameObject chainsawL;
    public GameObject chainsawR;
    private AudioSource chainsawSpeaker;
    private AudioSource louisRaySpeaker;

    public AudioClip[] chainsawSounds;
    public AudioClip[] louisRaySounds;
    public AudioClip spinSound;

    [SerializeField]
    private float _bossDistance;
    private bool _spinning;
    private bool _startedSpinning;
    private bool _startedWalking;
    private bool _hasSlashed;
    private bool _belowHalf;
    [SerializeField]
    private float _spinTimer;
    private float _walkTimer;
    private NavMeshAgent _agent;
    private Eddie _eddieScript;
    private Fast_Food_Worker _workerScript;
    

    new void Start()
    {
        base.Start();
        currentState = LouisRayStates.LouisRay_Walk;
        _agent = GetComponent<NavMeshAgent>();
        _spinTimer = Random.Range(3.0f, 9.0f);
        _walkTimer = Random.Range(3.0f, 9.0f);
        timer = wanderTimer;
        _eddieScript = Eddie.GetComponent<Eddie>();

        chainsawSpeaker = chainsawL.GetComponent<AudioSource>();
        louisRaySpeaker = gameObject.GetComponent<AudioSource>();
        _workerScript = worker.GetComponent<Fast_Food_Worker>();
    }

    public void BeginBattle()
    {
        _animator.enabled = true;
        currentState = LouisRayStates.LouisRay_Walk;
    }

    private void Update()
    {
        if (target != null)
        {
            _bossDistance = Vector3.Distance(transform.position, target.transform.position);
        }

        if (hitPoints <= (maxHitPoints/2))
        {
            _belowHalf = true;
            _animator.SetBool("below50", true);
            _animator.SetBool("above50", false);
        }

        if (hitPoints <= 0)
        {
            currentState = LouisRayStates.LouisRay_Dead;
        }

        switch (currentState)
        {
            case LouisRayStates.LouisRay_Idle:
                {
                    IdleState(Time.deltaTime);
                    break;
                }
            case LouisRayStates.LouisRay_Walk:
                {
                    WalkState(Time.deltaTime);
                    break;
                }
            case LouisRayStates.LouisRay_Slash:
                {
                    SlashState(Time.deltaTime);
                    break;
                }
            case LouisRayStates.LouisRay_Spin:
                {
                    SpinState(Time.deltaTime);
                    break;
                }
            case LouisRayStates.LouisRay_Dead:
                {
                    DeadState(Time.deltaTime);
                    break;
                }
        }
    }

    void IdleState(float deltaTime)
    {

    }
    void WalkState(float deltaTime)
    {
        if (!_startedWalking)
        {
            _animator.SetTrigger("walking");
            _startedWalking = true;
        }


        if (_bossDistance >= attackRange)
        {
          _agent.destination = target.transform.position;
            _walkTimer -= Time.deltaTime;
        }
        else if (_bossDistance <= attackRange || _walkTimer <= 0)
        {
            _walkTimer = 0;
            TransitionToNewState();
        }

    }
    void SlashState(float deltaTime)
    {
        if (!_hasSlashed)
        {
            _animator.SetTrigger("slashing");
        }

        transform.LookAt(target);
    }
    void SpinState(float deltaTime)
    {
        if (!_startedSpinning)
        {
            _animator.SetTrigger("spinning");
            
            _startedSpinning = true;
        }

        if (!_spinning)
        {
            _spinTimer -=deltaTime;
            transform.Rotate(0, 500 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
            if (timer >= wanderTimer)
            {
                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, spinRadius, -1);
                    _agent.SetDestination(newPos);
                    timer = 0;
                }
            }
        }

        if (_spinTimer <= 0)
        {
            _spinning = true;
            _animator.SetBool("doneSpinning", true);
        }
    }
    void DeadState(float deltaTime)
    {
        isDead = true;
        if (_eddieScript.isDead)
        {
            doorLocker.SetActive(false);
            _workerScript._dialogueValue++;
            kitchenDoor.GetComponent<OpenableInteractable>().isLocked = false;
            worker.SetActive(true);
            this.enabled = false;
            StartCoroutine(FadeBossMusic.StartFade(Audio_Manager.globalAudioManager.musicSoundArray[7].source, 5.0f, 0.0f));
            _eddieScript.enabled = false;
            
        }
    }

    public void TransitionToNewState()
    {
        float randAttack = Random.Range(0.0f , 1.0f);

        if (currentState == LouisRayStates.LouisRay_Slash && _belowHalf)
        {
            ResetAllVariables(); // do nothing except reset variables so that the followup can be achieved
        }

        if (currentState == LouisRayStates.LouisRay_Spin)
        {
            if (randAttack < 0.5f)
            {
                ResetAllVariables();
                chainsawSpeaker.Stop();
                currentState = LouisRayStates.LouisRay_Walk;
            }
            else
            {
                ResetAllVariables();
                chainsawSpeaker.Stop();
                currentState = LouisRayStates.LouisRay_Slash;
            }
        }
        else if (currentState == LouisRayStates.LouisRay_Slash)
        {
            if (randAttack < 0.5f)
            {
                ResetAllVariables();
                currentState = LouisRayStates.LouisRay_Walk;
            }
            else
            {
                ResetAllVariables();
                louisRaySpeaker.PlayOneShot(louisRaySounds[0]);
                chainsawSpeaker.PlayOneShot(spinSound);
               currentState = LouisRayStates.LouisRay_Spin;
            }
        }
        else if (currentState == LouisRayStates.LouisRay_Walk)
        {
            if (randAttack < 0.5f)
            {
                ResetAllVariables();
                louisRaySpeaker.PlayOneShot(louisRaySounds[0]);
                chainsawSpeaker.PlayOneShot(spinSound);
                currentState = LouisRayStates.LouisRay_Spin;
            }
            else
            {
                ResetAllVariables();
                currentState = LouisRayStates.LouisRay_Slash;
            }
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

    void ResetAllVariables()
    {
        _spinning = false;
        _startedSpinning = false;
        _startedWalking = false;
        _hasSlashed = false;
        _spinTimer = Random.Range(3.0f, 9.0f);
        _walkTimer = Random.Range(3.0f, 9.0f);
        _animator.ResetTrigger("spinning");
        _animator.ResetTrigger("walking");
        _animator.ResetTrigger("slashing"); 
        _animator.SetBool("doneSpinning", false);
    }

    void EnableChainsawHitbox(int enabled)
    {
        if (enabled == 1)
        {
            chainsawL.GetComponent<BoxCollider>().enabled = true;
            chainsawR.GetComponent<BoxCollider>().enabled = true;

            if (currentState == LouisRayStates.LouisRay_Slash)
            {
                int randomAttackSound = Random.Range(0, chainsawSounds.Length);
                chainsawSpeaker.PlayOneShot(chainsawSounds[randomAttackSound]);
            }
        }
        else
        {
            chainsawL.GetComponent<BoxCollider>().enabled = false;
            chainsawR.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChainsawHBox")
        {
            _contactPoint = other.gameObject;
            TakeDamage("chainsaw", _contactPoint.GetComponent<PlayerDamageRef>().GetPlayerDamage(), _contactPoint.transform.position, _contactPoint.transform.forward);
        }
    }
}
