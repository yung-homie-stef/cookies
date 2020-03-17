using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
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

    public void Die(Vector3 impulse)
    {
        foreach ( var body in childrenBody)
        {
            body.isKinematic = false;
        }

        _animator.enabled = false;
    }
}
