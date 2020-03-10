using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Die()
    {
        _animator.enabled = false;
        _rigidbody.detectCollisions = false;
        _rigidbody.isKinematic = true;

    }
}
