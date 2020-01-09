using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    public bool movementEnabled = true;
  
    private float _translation;
    private float _strafe;
    private Animator _animator;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (movementEnabled)
        {
            _translation = Input.GetAxis("Vertical") * speed;
            _strafe = Input.GetAxis("Horizontal") * speed;
            _translation *= Time.deltaTime;
            _strafe *= Time.deltaTime;

            transform.Translate(_strafe, 0, _translation);

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