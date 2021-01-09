using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : MonoBehaviour
{
    public static int hogsLeft = 12;
    public GameObject cop_ensemble;
    public GameObject player;
    public GameObject computer;

    private Animator _animator;
    private bool _hasDecremented;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt(player.transform.position);

        if (_animator.enabled == false && _hasDecremented == false)
        {
            hogsLeft--;
            _hasDecremented = true;

            Debug.Log(hogsLeft);

            if (hogsLeft == 0)
            {
                StartCoroutine(RemoveCops(5.0f));
            }
        }
    }

    private void OnEnable()
    {
        GenerateShootingLength();
    }

    private void GenerateShootingLength()
    {
        StartCoroutine(StandOrSitAndShoot(Random.Range(0, 5)));
    }

    private IEnumerator RemoveCops(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        computer.SetActive(true);
        cop_ensemble.SetActive(false);    

    }

    private IEnumerator StandOrSitAndShoot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _animator.SetInteger("shoot_num", Random.Range(1, 3));
        GenerateShootingLength();
    }

    public void SetShootNumberToZero()
    {
        _animator.SetInteger("shoot_num", 0);
    }



}
