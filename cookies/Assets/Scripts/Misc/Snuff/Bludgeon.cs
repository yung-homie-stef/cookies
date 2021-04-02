using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bludgeon : MonoBehaviour
{
    public GameObject intercomCamera;
    public GameObject intercom;
   

    private AudioSource _muffledMans;
    [SerializeField]
    private int _bludgeons = 0;
    private Rigidbody[] _childBodies;
    private GameObject _contactPoint;

    public AudioClip[] crunchHitSounds;

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
                PlayCrunchNoise();

                if (_bludgeons < 5)
                {
                    StartCoroutine(GoBackToIdle(0.5f));
                }
                else 
                {
                    PlayCrunchNoise();
                    GetComponent<Victim>().TakeDamage("melee", 1, _contactPoint.transform.position, _contactPoint.transform.forward * 0.2f);
                    StartCoroutine(KillSound(1.0f));
                    KillMans();
                }

            }
        }     
    }

    private IEnumerator GoBackToIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

            GetComponent<Animator>().SetBool("bludgeoned", false);
    }

    void KillMans()
    {
        intercom.GetComponent<BoxCollider>().enabled = true;
        intercomCamera.SetActive(true);
        GetComponent<Animator>().enabled = false;
        
    }

    void PlayCrunchNoise()
    {
        int randomAttackSound = Random.Range(0, crunchHitSounds.Length);
        GetComponent<AudioSource>().PlayOneShot(crunchHitSounds[randomAttackSound]);
    }

    IEnumerator KillSound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<AudioSource>().enabled = false;
    }
}
