using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Grenade : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform normanHand;
    public float throwForce = 50f;

    private IEnumerator ThrowSmoke(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject gren = Instantiate(grenadePrefab, normanHand.position, normanHand.rotation) as GameObject;
        gren.GetComponent<Rigidbody>().AddForce(-normanHand.forward * throwForce, ForceMode.Impulse);
    }

   public void SmokeInvoking()
    {
        StartCoroutine(ThrowSmoke(1.0f));
    }
}
