using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Victim : MonoBehaviour
{
    public static float multiplier = 10;

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

    public void Die(Vector3 point = default(Vector3), Vector3 direction = default(Vector3))
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
}
