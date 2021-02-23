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

    new void Start()
    {
        base.Start();
        currentState = NormanStates.NormanState_Walk;
        _agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public void BeginBattle()
    {
        
    }

    private void Update()
    {
        if (hitPoints == 0)
        {
            currentState = NormanStates.NormanState_Dead;
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
    }

    void CrawlState(float time)
    {
        if (!_hasCrouched)
        {
            _animator.SetBool("prone", true);
            _hasCrouched = true;
            _actionChangeTimer = ChangeActionTimer(6.0f, 10.0f);
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
            int _newState = Random.Range(1, 3);
            switch (_newState)
            {
                case 1:
                    {
                        currentState = NormanStates.NormanState_Crawl;
                        break;
                    }
                case 2:
                    {
                        currentState = NormanStates.NormanState_Walk;
                        break;
                    }
                case 3:
                    {
                        currentState = NormanStates.NormanState_Smoke;
                        break;
                    }
            }
            
        }
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Walk)
        {
            int _newState = Random.Range(1, 3);
            switch (_newState)
            {
                case 1:
                    {
                        currentState = NormanStates.NormanState_Crawl;
                        break;
                    }
                case 2:
                    {
                        currentState = NormanStates.NormanState_Smoke;
                        break;
                    }
                case 3:
                    {
                        currentState = NormanStates.NormanState_Shoot;
                        break;
                    }
            }

        }
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Smoke)
        {
            int _newState = Random.Range(1, 3);
            switch (_newState)
            {
                case 1:
                    {
                        currentState = NormanStates.NormanState_Crawl;
                        break;
                    }
                case 2:
                    {
                        currentState = NormanStates.NormanState_Shoot;
                        break;
                    }
                case 3:
                    {
                        currentState = NormanStates.NormanState_Walk;
                        break;
                    }
            }

        }
        #endregion
        #region
        else if (currentState == NormanStates.NormanState_Melee)
        {
            _hasMeleed = false;
            _agent.isStopped = false;
            int _newState = Random.Range(1, 4);
            switch (_newState)
            {
                case 1:
                    {
                        currentState = NormanStates.NormanState_Crawl;
                        break;
                    }
                case 2:
                    {
                        currentState = NormanStates.NormanState_Shoot;
                        break;
                    }
                case 3:
                    {
                        currentState = NormanStates.NormanState_Walk;
                        break;
                    }
                case 4:
                    {
                        currentState = NormanStates.NormanState_Smoke;
                        break;
                    }
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

}
