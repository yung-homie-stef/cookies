using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform cucoHand;
    public float throwForce = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ThrowGrenade(0.5f));
            gameObject.GetComponent<Animator>().SetTrigger("revert");
        }
    }

    private IEnumerator ThrowGrenade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject gren = Instantiate(grenadePrefab, cucoHand.position, cucoHand.rotation) as GameObject;
        gren.GetComponent<Rigidbody>().AddForce(-cucoHand.forward * throwForce, ForceMode.Impulse);
    }
}
