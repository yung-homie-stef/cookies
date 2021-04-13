using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Victim : MonoBehaviour
{
    public static float multiplier = 5;
    public int hitPoints;
    public int maxHitPoints;
    public string[] damageTypes;
    public bool isBoss = false;
    public HealthBar healthbar;

    public ParticleSystem bloodPrefab;
    ParticleSystem tempBlood = null;

    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected CapsuleCollider _capsuleCollider;

    private GameObject _contactPoint;
    protected Collider[] childrenCollider;
    protected Rigidbody[] childrenBody;
    protected BoxCollider boxCollider;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        childrenCollider = GetComponentsInChildren<Collider>();
        childrenBody = GetComponentsInChildren<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        hitPoints = maxHitPoints;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(hitPoints);
        }
    }

    public virtual void Die(Vector3 point = default(Vector3), Vector3 direction = default(Vector3))
    {
        foreach ( var body in childrenBody)
        {
            float bulletDistance = Vector3.Distance(body.position, point);
            float deathForceMultiplier = Mathf.Max(multiplier - (bulletDistance * 5), 0); // the closer the bullet to the body, the greater the force

            body.isKinematic = false;
            body.AddForceAtPosition((direction * deathForceMultiplier), point, ForceMode.Impulse);
        }

        _animator.enabled = false;
        boxCollider.enabled = false;

        // if victim has a navmesh disable it so it doesn't get wacky
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;       
        }

    }

    public virtual void TakeDamage(string dmgType, int dmgNum, Vector3 point = default(Vector3), Vector3 direction = default(Vector3))
    {
        for (int i=0; i < damageTypes.Length; i++)
        {
            if (damageTypes[i] == dmgType)
            {
                hitPoints -= dmgNum;

                tempBlood = Instantiate(bloodPrefab, point, transform.rotation);
                Destroy(tempBlood.gameObject, 0.5f);

                if (isBoss)
                {
                    healthbar.SetHealth(hitPoints);
                }

                if (hitPoints == 0)
                {
                    Die(point, direction);

                    if (isBoss)
                    {
                        healthbar.gameObject.SetActive(false);
                    }
                }
                break;
 
            }

        }  
    }

    public virtual void Die() // overloaded function for when victim dies without being hit or shot (ex: poison)
    {
        foreach (var body in childrenBody)
        {
            body.isKinematic = false;
        }

        if (GetComponent<Interactable>())
        {
            GetComponent<Interactable>().enabled = false;
        }

        // if victim has a navmesh disable it so it doesn't get wacky
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            _contactPoint = other.gameObject;
            TakeDamage("melee", _contactPoint.GetComponent<PlayerDamageRef>().GetPlayerDamage(), _contactPoint.transform.position, _contactPoint.transform.forward);

            int meleeSound = Random.Range(1, 4);
            Audio_Manager.globalAudioManager.PlaySound("flesh_" + meleeSound, Audio_Manager.globalAudioManager.meleeSoundArray);
        }
    }

}
