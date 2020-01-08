using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
  
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

        _translation = Input.GetAxis("Vertical") * speed;
        _strafe = Input.GetAxis("Horizontal") * speed;
        _translation *= Time.deltaTime;
        _strafe *= Time.deltaTime;

        transform.Translate(_strafe, 0, _translation);


    }

}