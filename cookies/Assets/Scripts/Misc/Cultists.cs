using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultists : MonoBehaviour
{
    public int animationInt;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("praise_animation", animationInt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
