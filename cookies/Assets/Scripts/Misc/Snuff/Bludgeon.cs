using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bludgeon : MonoBehaviour
{
    public GameObject intercomCamera;
    public GameObject intercom;

    private int _bludgeons = 0;
    private Rigidbody[] _childBodies;
    private GameObject _contactPoint;

    private void Start()
    {
        _childBodies = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            if (other.name == "brass_knuckle(Clone)")
            {
                _contactPoint = other.gameObject;
                _bludgeons++;
                GetComponent<Animator>().SetBool("bludgeoned", true);
                StartCoroutine(GoBackToIdle(0.5f));
            }
        }     
    }

    private IEnumerator GoBackToIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (_bludgeons <= 5)
        {
            GetComponent<Animator>().SetBool("bludgeoned", false);
            GetComponent<Victim>().TakeDamage(_contactPoint.transform.position, _contactPoint.transform.forward * 0.2f);
        }
        
        if (_bludgeons == 5)
        {       
            intercom.GetComponent<BoxCollider>().enabled = true;
            intercomCamera.SetActive(true);
            GetComponent<Animator>().enabled = false;
        }
    }
}
