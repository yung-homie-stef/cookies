﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nazi : Victim
{
    public enum NaziStates
    {
        NaziState_Idle,
        NaziState_Chase,
        NaziState_Slash,
        NaziState_Kick,
        NaziState_Stab,
        NaziState_Dead
    }

    public NaziStates currentState = NaziStates.NaziState_Idle;

    public Transform target = null;
    public float attackRange = 1.0f;
    public float movementSpeed = 1.0f;
    public bool isBelowHalfHealth = false;
    public GameObject keyring;
    public GameObject finalDoor;
    public GameObject naziBoot;
    public GameObject naziDagger;
    

    public int slashDmg = 1;
    public int kickDmg = 1;
    public int stabDmg = 1;

    public float inRadiusTimer = 1.5f;
    public float radiusTimerReset = 1.5f;

    [SerializeField]
    private float _bossDistance;
    private bool _hasSlashed = false;
    private bool _hasStabbed = false;
    private bool _hasDied = false;

    private GameObject _contactPoint;
    private BoxCollider _boxCollider;
    private Boot _boot;
    private BossWeapon _dagger;

    new void Start()
    {
        base.Start();
        _boot = naziBoot.GetComponent<Boot>();
        _dagger = naziDagger.GetComponent<BossWeapon>();
    }

    public void BeginBattle()
    {
        currentState = NaziStates.NaziState_Chase;
    }

    private void Update()
    {
        if (target !=null)
        {
            transform.LookAt(target);
            _bossDistance = Vector3.Distance(transform.position, target.transform.position);
        }

        if (hitPoints <= (maxHitPoints/2))
        {
            isBelowHalfHealth = true;
            movementSpeed = 1.25f;
        }

        if (_bossDistance >= attackRange)
        {
            currentState = NaziStates.NaziState_Chase;
        }

        if (hitPoints == 0)
        {
            currentState = NaziStates.NaziState_Dead;
        }



        switch (currentState)
        {
            case NaziStates.NaziState_Idle:
                {
                    IdleState(Time.deltaTime);
                    break;
                }
            case NaziStates.NaziState_Chase:
                {
                    ChaseState(Time.deltaTime);
                    break;
                }
            case NaziStates.NaziState_Stab:
                {
                    StabState(Time.deltaTime);
                    break;
                }
            case NaziStates.NaziState_Kick:
                {
                    KickState(Time.deltaTime);
                    break;
                }
            case NaziStates.NaziState_Slash:
                {
                    SlashState(Time.deltaTime);
                    break;
                }
            case NaziStates.NaziState_Dead:
                {
                    DieState(Time.deltaTime);
                    break;
                }
            default:
                {
                    IdleState(Time.deltaTime);
                    break;
                }
        }
    }

    void IdleState(float deltaTime)
    {
        // nothing
    }
    void ChaseState(float deltaTime)
    {
        _hasSlashed = false;
        inRadiusTimer = radiusTimerReset;
        _animator.SetBool("walking", true);

        if (_bossDistance >= attackRange)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
        else
        {
            if (!isBelowHalfHealth)
            {
                _hasSlashed = false;
                currentState = NaziStates.NaziState_Slash;
            }
            else
            {
                if (Random.value < 0.5f)
                    currentState = NaziStates.NaziState_Slash;
                else
                    currentState = NaziStates.NaziState_Stab; 

            }
        }
    }
    void StabState(float deltaTime)
    {
        _animator.SetBool("walking", false);
        _animator.SetTrigger("stabbing");

        // plays stabbing animation
        // enable dagger hitbox
        // dmg player if successful hit
        // starts timer for kick
        // can transition to slash when below 50%
        // switch to kick or slash when animation is done playing
        // transition to chase if player exits range during attack
    }
    void KickState(float deltaTime)
    {
        _animator.SetBool("walking", false);
        _animator.SetTrigger("kicking");


        // plays kicking animation
        // enable foot hitbox
        // pushes player away upon contact with foot
        // dmg player if successful hit
        // resume chasing when players exits radius
        // loop kicking if player doesnt exit radius
        // transition to stab when below 50%
        // resets kick timer
    }
    void SlashState(float deltaTime)
    {
        // plays stabbing animation
        // enable dagger hitbox
        // dmg player if successful hit
        // starts timer for kick
        // can transition to stab when below 50%
        // switch to kick or slash when animation is done playing
        // transition to chase if player exits range during attack

        _animator.SetBool("walking", false);

        if (!_hasSlashed)
        {
            _animator.SetTrigger("slashing");
            _hasSlashed = true;
        }

        inRadiusTimer -= Time.deltaTime;

        if (inRadiusTimer <= 0)
        {
            currentState = NaziStates.NaziState_Kick;
            inRadiusTimer = radiusTimerReset;
            _animator.SetTrigger("kicking");
        }

    }
    void DieState(float deltaTime)
    {
        if (!_hasDied)
        {
            StartCoroutine(GivePlayerNaziKey(1.5f));
            _hasDied = true;
        }
    }

    private IEnumerator GivePlayerNaziKey(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        keyring.transform.parent = null;
        keyring.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
        keyring.tag = "Interactable";
        finalDoor.tag = "Interactable";
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            _contactPoint = other.gameObject;
            GetComponent<Victim>().TakeDamage("melee", _contactPoint.transform.position, _contactPoint.transform.forward);

        }
    }

    public void EnableBootHitbox(int flag)
    {
        // call boot method
        _boot.EnableBootCollider(flag);
    }

    public void EnableDaggerHitbox(int flag)
    {
        // call dagger method
        _dagger.EnableWeaponCollider(flag);
    }

    public void RandomizeNextTransition()
    {
        if (currentState == NaziStates.NaziState_Stab)
        {
            if (Random.value < 0.5f)
                currentState = NaziStates.NaziState_Slash;
            else
                currentState = NaziStates.NaziState_Kick;
        }
        else if (currentState == NaziStates.NaziState_Kick)
        {
            if (Random.value < 0.5f)
                currentState = NaziStates.NaziState_Slash;
            else
                currentState = NaziStates.NaziState_Stab;
        }
        else if (currentState == NaziStates.NaziState_Slash)
        {
            if (Random.value < 0.5f)
                currentState = NaziStates.NaziState_Kick;
            else
                currentState = NaziStates.NaziState_Stab;
        }
    }


}
