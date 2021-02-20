using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    private RaycastHit _hit;
    public GameObject player;
    public float rangeSqr;

    private float _shootTimer = 2.0f;
    private float _rayDistance = 100.0f;
    private Animator _animator;
    private bool _notShooting = true;
    private Ray _ray;
    private Vector3 _gunmanRayStart;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("combat", true);
        _gunmanRayStart = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_gunmanRayStart, transform.forward * 3, Color.green);
        float distanceSqr = (gameObject.transform.position - player.transform.position).sqrMagnitude;
        if (distanceSqr < rangeSqr)
        {
            if (_notShooting)
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= 2.0f)
            {
                _notShooting = false;
                ShootAtPlayer();
                StartCoroutine(ResetShootTimer(1.0f));
            }
            else
            {  
                transform.LookAt(player.transform);
            }
        }
    }

    void ShootAtPlayer()
    {
        gameObject.GetComponent<Animator>().SetTrigger("shooting");
        _notShooting = true;
        
        if (Physics.Raycast(_gunmanRayStart, transform.forward, out _hit, _rayDistance))
        {    
            //raycast forward and see...
            if ( _hit.collider.gameObject.tag == "Player")
            {        
                //whether it is a player...
                Debug.Log("gotcha bitch");  

                // make player take damage
            }
        }   
    }

    private IEnumerator ResetShootTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _shootTimer = 0.0f;
    }
}
