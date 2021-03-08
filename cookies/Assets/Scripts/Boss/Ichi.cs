using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ichi : Victim
{
    public enum IchiStates
    {
        Ichi_Idle,
        Ichi_Walk,
        Ichi_Kicking,
        Ichi_Kunai,
        Ichi_Crawl,
        Ichi_Hump,
        Ichi_Dead

    }

    public Transform target = null;
    public IchiStates currentState;
    public float attackRange = 1.0f;
    public float crawlSpeed = 1.5f;
    public GameObject headItem;
    public GameObject mask;
    public GameObject head;
    public GameObject yakuzaBoss;
    public GameObject doorLocker;
    public OpenableInteractable sadomasoDoor;

    public BoxCollider dickBlade;
    public BoxCollider rightHand;
    public BoxCollider leftFoot;
    public BoxCollider rightFoot;

    [SerializeField]
    private float _bossDistance;
    private float _kickNumber;
    [SerializeField]
    private float _walkTimer = 7.0f;
    [SerializeField]
    private float _idleTimer = 1.5f;
    private bool _startedWalking;
    private bool _transKicked;
    private bool _kicked;
    private bool _thrown;
    private bool _crawling;
    private bool _isDead;
    private NavMeshAgent _agent;
    private Yakuza_Boss _yakuzaScript;
    private AudioSource _source;

    public AudioClip[] kickSounds;
    public AudioClip[] humpSounds;
    public AudioClip[] throwSounds;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        currentState = IchiStates.Ichi_Walk;
        _agent = GetComponent<NavMeshAgent>();
        _yakuzaScript = yakuzaBoss.GetComponent<Yakuza_Boss>();
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            _bossDistance = Vector3.Distance(transform.position, target.transform.position);
        }

        if (hitPoints <= (maxHitPoints/2))
        {
            if (!_crawling)
            {
                _agent.isStopped = true;
                _animator.SetBool("below50", true);
                _agent.speed += .25f;
                StartCoroutine(SetIchiToCrawl(2.0f));
                _crawling = true;
            }
        }

        if (hitPoints == 0)
        {
            currentState = IchiStates.Ichi_Dead;
        }

        switch (currentState)
        {
            case IchiStates.Ichi_Idle:
            {
                IdleState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Walk:
            {
                WalkState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Kicking:
            {
                KickingState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Kunai:
            {
                KunaiState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Crawl:
            {
                CrawlState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Hump:
            {
                HumpState(Time.deltaTime);
                break;
            }
            case IchiStates.Ichi_Dead:
            {
                DeadState(Time.deltaTime);
                break;
            }

        }

    }

    void IdleState(float deltatime)
    {
        _agent.isStopped = true;

        if (_idleTimer > 0)
        _idleTimer -= Time.deltaTime;

        if (_idleTimer <= 0)
        {
            if (Random.Range(0.0f, 1.0f) <= 0.5f)
            {
                _walkTimer = 7.0f;
                _startedWalking = false;
                _transKicked = false;
                currentState = IchiStates.Ichi_Walk;
            }
            else
            {
                _thrown = false;
                currentState = IchiStates.Ichi_Kunai;
            }
        }
    }
    void WalkState(float deltatime)
    {
        bool _timerEnabled = true;
        _agent.isStopped = false;

        transform.LookAt(target);

        if (_timerEnabled)
        {
            _walkTimer -= Time.deltaTime;

            if (_walkTimer < 0)
            {
                _thrown = false;
                currentState = IchiStates.Ichi_Kunai;
                _walkTimer = 0;
            }
        }

        if (!_startedWalking)
        {
            _animator.SetTrigger("walking");
            _startedWalking = true;
        }

        if (_bossDistance >= attackRange)
        {
            _agent.destination = target.transform.position;
            
        }

        if (_bossDistance <= attackRange)
        {
            _timerEnabled = false;
            if (!_transKicked)
            {
                ChooseCloseRangeAttack();
                _transKicked = true;
            }
        }
    }
    void KickingState(float deltatime)
    {
        transform.LookAt(target);
        _agent.isStopped = true;

        if (!_kicked)
        {
            if (_kickNumber <= 0.5f)
            {
                _animator.SetTrigger("kicking");
            }
            else
                _animator.SetTrigger("comboing");

            _kicked = true;
        }
    }
    void KunaiState(float deltatime)
    {
        _agent.isStopped = true;
        transform.LookAt(target);

        if (!_thrown)
        {
            _animator.SetTrigger("throwing");
            AttackSounds(throwSounds);
            _thrown = true;
        }
    }
    void CrawlState(float deltatime)
    {
        _agent.isStopped = false;
        

        if (_bossDistance >= attackRange)
        {
            _agent.destination = target.transform.position;
        }
        else if (_bossDistance <= attackRange )
        {
            // hump
            _animator.SetTrigger("humping");
            currentState = IchiStates.Ichi_Hump;
        }
    }
    void HumpState(float deltatime)
    {
        _agent.isStopped = true;
    }
    void DeadState(float deltatime)
    {
        if (!_isDead)
        {
            head.SetActive(false);
            mask.SetActive(false);
            doorLocker.SetActive(false);
            sadomasoDoor.isLocked = false;
            StartCoroutine(GiveIchisHead(1.5f));
            _isDead = true;
        }
    }

    void ChooseCloseRangeAttack()
    {
            _kickNumber = Random.Range(0.0f, 1.0f);
            _kicked = false;
            currentState = IchiStates.Ichi_Kicking;  
    }

    public void ReturnToIdle()
    {
        _idleTimer = 1.5f;
        currentState = IchiStates.Ichi_Idle;
    }

    private IEnumerator GiveIchisHead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _yakuzaScript.reqType = Interactable.RequirementType.Single;
        _yakuzaScript.requiredTags = new string[1];
        _yakuzaScript.requiredTags[0] = "Head";
        headItem.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
        headItem.tag = "Interactable";
    }

    private IEnumerator SetIchiToCrawl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentState = IchiStates.Ichi_Crawl;
    }

    public void ReturnToCrawl()
    {
        currentState = IchiStates.Ichi_Crawl;
    }

    public void EnableDickBladeHitbox(int param)
    {
        if (param == 1)
        {
            dickBlade.enabled = true;
            AttackSounds(humpSounds);
        }
        else
            dickBlade.enabled = false;
    }
    public void EnableRightFistHitbox(int param)
    {
        if (param == 1)
        {
            rightHand.enabled = true;
            AttackSounds(kickSounds);
        }
        else
            rightHand.enabled = false;
    }
    public void EnableRightFootHitbox(int param)
    {
        if (param == 1)
        {
            rightFoot.enabled = true;
            AttackSounds(kickSounds);
        }
        else
            rightFoot.enabled = false;
    }
    public void EnableLeftFootHitbox(int param)
    {
        if (param == 1)
        {
            leftFoot.enabled = true;
            AttackSounds(kickSounds);
        }
        else
            leftFoot.enabled = false;
    }

    public void AttackSounds(AudioClip[] array)
    {
        int randomAttackSound = Random.Range(0, array.Length);
        _source.PlayOneShot(array[randomAttackSound]);
    }

}
