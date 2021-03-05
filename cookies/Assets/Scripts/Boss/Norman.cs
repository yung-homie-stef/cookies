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
        NormanState_Crawl,
        NormanState_Dead
    }

    public NormanStates currentState = NormanStates.NormanState_Idle;

    public Transform target = null;
    public float wanderRadius;
    public float wanderTimer;
    public float meleeRange = 1.0f;
    public GameObject rifle;
    public End_Condition gladBoysThread;
    public BoxCollider ar15;

    [SerializeField]
    private float _bossDistance;
    [SerializeField]
    private float _actionChangeTimer;
    private int shootNumber = 0;
    private NavMeshAgent _agent;
    private float timer;

    private GameObject _contactPoint;
    private bool _hasCrouched;
    private bool _hasMeleed;
    private bool _hasShot;
    private bool _hasBegunWalking;
    private bool _hasDied;
    private bool _hasSmoked;

    private Smoke_Grenade _smokeScript;

    new void Start()
    {
        base.Start();
        currentState = NormanStates.NormanState_Walk;
        _agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        _smokeScript = gameObject.GetComponent<Smoke_Grenade>();
    }

    private void Update()
    {
        if (hitPoints == 0)
        {
            currentState = NormanStates.NormanState_Dead;
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            _smokeScript.SmokeInvoking();
        }


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
            case NormanStates.NormanState_Dead:
            {
                DeadState(Time.deltaTime);
                break;
            }
        }

        if (target != null)
        {
            _bossDistance = Vector3.Distance(transform.position, target.transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            _contactPoint = other.gameObject;
            GetComponent<Victim>().TakeDamage("melee", _contactPoint.transform.position, _contactPoint.transform.forward);
        }
    }

    void IdleState(float time)
    {

    }

    void WalkState(float time)
    {
        if (!_hasBegunWalking)
        {
            _actionChangeTimer = ChangeActionTimer(3.0f, 6.0f);
            _hasBegunWalking = true;
        }

        _actionChangeTimer -= Time.deltaTime;

        timer += Time.deltaTime;
        _agent.speed = 1f;
        _agent.isStopped = false;

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

        if (_actionChangeTimer <= 0)
        {
            _actionChangeTimer = 0;
            _hasBegunWalking = false;
            TransitionToNewState();
        }
    }

    void ShootState(float time)
    {
        _agent.isStopped = true;
        transform.LookAt(target);

        if (!_hasShot)
        {
            _animator.SetTrigger("shooting");
            _hasShot = true;
        }
    }

    void MeleeState(float time)
    {
        if (!_hasMeleed)
        {
            _animator.SetTrigger("melee");
            _hasMeleed = true;
        }

        _agent.isStopped = true;
        transform.LookAt(target);
    }

    void SmokeState(float time)
    {
        _animator.SetTrigger("smoking");
        _agent.isStopped = true;
        transform.LookAt(target);

        if (!_hasSmoked)
        {
            _smokeScript.SmokeInvoking();
            _hasSmoked = true;
        }
    }

    void CrawlState(float time)
    {
        if (!_hasCrouched)
        {
            _animator.SetBool("prone", true);
            _hasCrouched = true;
            _actionChangeTimer = ChangeActionTimer(3.0f, 6.0f);
            _agent.isStopped = false;
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
            _hasCrouched = false;
            currentState = NormanStates.NormanState_Walk;
        }
    }

    void DeadState(float time)
    {
        if (!_hasDied)
        {
            _agent.isStopped = true;
            rifle.transform.parent = null;
            StartCoroutine(EndNormansThread(7.0f));
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

    public void TransitionToNewState()
    {
        // STATE TRANSITIONING
        #region 
        #region
        if (currentState == NormanStates.NormanState_Shoot)
        {
            float _newState = Random.Range(0.0f, 1f);
            if (_newState <= .15f)
            {
               currentState = NormanStates.NormanState_Crawl;
            }
            else if (_newState > .15f && _newState < .6f)
            {
               currentState = NormanStates.NormanState_Smoke;
            }
            else 
            {
            currentState = NormanStates.NormanState_Walk;         
            }
        }
            
     
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Walk)
        {
            float _newState = Random.Range(0.0f, 1f);
            if (_newState <= .15f)
            {
                currentState = NormanStates.NormanState_Crawl;
            }
            else if (_newState > .15f && _newState < .6f)
            {
                currentState = NormanStates.NormanState_Smoke;
            }
            else
            {
                currentState = NormanStates.NormanState_Shoot;
            }
        }
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Smoke)
        {

            float _newState = Random.Range(0.0f, 1f);
            if (_newState <= .33f)
            {
                currentState = NormanStates.NormanState_Crawl;
                _hasSmoked = false;
            }
            else if (_newState > .33f && _newState < .7f)
            {
                currentState = NormanStates.NormanState_Shoot;
                _hasSmoked = false;
            }
            else
            {
                currentState = NormanStates.NormanState_Walk;
                _hasSmoked = false;
            }

        }
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Melee)
        {
            _hasMeleed = false;
            _agent.isStopped = false;
            float _newState = Random.Range(0.0f, 1f);
            if (_newState <= .15f)
            {
                currentState = NormanStates.NormanState_Crawl;
            }
            else if (_newState > .15f && _newState <= .4f)
            {
                currentState = NormanStates.NormanState_Smoke;
            }
            else if(_newState > .4f && _newState <= .75f)
            {
                currentState = NormanStates.NormanState_Walk;
            }
            else
            {
                currentState = NormanStates.NormanState_Shoot;
            }

        }
        #endregion
        #endregion

    }   

    public void CountTheShots()
    {
        if (shootNumber <= 3)
        {
            shootNumber++;
            _hasShot = false;
        }
        else
        {
            _hasShot = false;
            shootNumber = 0;
            TransitionToNewState();
            _agent.isStopped = false;
        }
    }

    private IEnumerator EndNormansThread(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(gladBoysThread);
    }

    public void EnableGunHitbox(int param)
    {
        if (param == 1)
            ar15.enabled = true;
        else
            ar15.enabled = false;
    }

}
