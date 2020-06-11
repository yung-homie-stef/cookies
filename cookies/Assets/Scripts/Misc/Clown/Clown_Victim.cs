using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clown_Victim : Victim
{
    private bool stunned = false;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void TakeDamage(Vector3 point = default(Vector3), Vector3 direction = default(Vector3))
    {

        if (!stunned)
        hitPoints--;

        if (hitPoints == 0)
        {
            stunned = true;
            _animator.SetBool("stunned", true);
        }
    }

    public void RelieveOfStun()
    {
        stunned = false;
        _animator.SetBool("stunned", false);
    }

    private new void Die() // overloaded function for when victim dies without being hit or shot (ex: poison)
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
