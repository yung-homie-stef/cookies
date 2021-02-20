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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    StartCoroutine(ThrowGrenade(0.5f));
        //    gameObject.GetComponent<Animator>().SetTrigger("revert");
        //}

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
