using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Victim : MonoBehaviour
{
    public static float multiplier = 10;
    public int hitPoints;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    protected Collider[] childrenCollider;
    protected Rigidbody[] childrenBody;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        childrenCollider = GetComponentsInChildren<Collider>();
        childrenBody = GetComponentsInChildren<Rigidbody>();
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

        // if victim has a navmesh disable it so it doesn't get wacky
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;       
        }

    }

    public virtual void TakeDamage(Vector3 point = default(Vector3), Vector3 direction = default(Vector3))
    {
        hitPoints--;

        if (hitPoints == 0)
        Die(point, direction);
    }

    public virtual void Die() // overloaded function for when victim dies without being hit or shot (ex: poison)
    {
        foreach (var body in childrenBody)
        {
            body.isKinematic = false;
        }

        // if victim has a navmesh disable it so it doesn't get wacky
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
