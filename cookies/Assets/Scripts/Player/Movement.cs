﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public bool movementEnabled = true;
  
    private float _translation;
    private float _strafe;
    private Animator _animator;
    private Rigidbody _playerRB;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();

        if (_playerRB = GetComponent<Rigidbody>())
        {
            Debug.Log("RB sucessfully acquired");
        }
        else
        {
            Debug.Log("failed to get the RB you dumbass");
        }
        
    }

    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (movementEnabled)
        {
            Vector3 forwardDirection = transform.forward;
            Vector3 strafeDirection = transform.right;

            Vector3 movementDirection = Vector3.zero;

            _translation = Input.GetAxisRaw("Vertical");
            _strafe = Input.GetAxisRaw("Horizontal");
            _translation *= Time.deltaTime;
            _strafe *= Time.deltaTime;

            movementDirection = ((forwardDirection * _translation) + (strafeDirection * _strafe)).normalized * speed; 

            //transform.Translate(_strafe, 0, _translation);
            _playerRB.AddForce(movementDirection, ForceMode.VelocityChange); 
        }
    }

    private void Update()
    {
        // TODO: fix with blend tree
        if (movementEnabled)
        {
            if (_translation > 0)
                _animator.SetInteger("direction", 1);
            else if (_translation < 0)
                _animator.SetInteger("direction", 2);
            else if (_strafe > 0)
                _animator.SetInteger("direction", 3);
            else if (_strafe < 0)
                _animator.SetInteger("direction", 4);
            else
                _animator.SetInteger("direction", 0);
        }
    }

}