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
    private float grenadeTimer = 0.0f;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private Cartel_Member _cartelScript;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _cartelScript = gameObject.GetComponent<Cartel_Member>();
    }

    private void Update()
    {
        float distanceSqr = (gameObject.transform.position - player.transform.position).sqrMagnitude;
        if (distanceSqr < rangeSqr)
        {
            transform.LookAt(player.transform);
            grenadeTimer += Time.deltaTime;
            if (grenadeTimer >= 2f)
            {
                GrenadeInvoking();
                grenadeTimer = 0.0f;
            }
        }

        if (!_animator.enabled)
        {
            _boxCollider.enabled = false;
            this.enabled = false;
            _cartelScript.ReduceMemberNumber();
        }

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
        gameObject.GetComponent<Animator>().SetTrigger("revert");
    }
}
