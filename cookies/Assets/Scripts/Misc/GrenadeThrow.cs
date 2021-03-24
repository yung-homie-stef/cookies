using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform cucoHand;
    public float throwForce = 10f;
    public GameObject player;
    public float rangeSqr;

    private bool _throwing;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private Cartel_Member _cartelScript;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _cartelScript = gameObject.GetComponent<Cartel_Member>();
    }

    private IEnumerator ThrowGrenade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject gren = Instantiate(grenadePrefab, cucoHand.position, cucoHand.rotation) as GameObject;
        gren.GetComponent<Rigidbody>().AddForce(-cucoHand.forward * throwForce, ForceMode.Impulse);
    }

    private void GrenadeInvoking()
    {
        StartCoroutine(ThrowGrenade(0.5f));
    }
}
