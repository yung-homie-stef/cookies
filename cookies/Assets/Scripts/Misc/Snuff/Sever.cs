using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sever : MonoBehaviour
{
    public GameObject delegatedBone;
    public GameObject poorGuy;
    public GameObject intercom;
    public GameObject intercomCamera;

    private static int _limbsCut = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            Vector3 originalBonePos = delegatedBone.transform.position;
            delegatedBone.transform.parent = null; // detach limb
            GetComponent<BoxCollider>().enabled = false;
            delegatedBone.transform.position = originalBonePos;

            delegatedBone.GetComponent<Rigidbody>().isKinematic = false; // enable physics
            poorGuy.GetComponent<Animator>().SetBool("severed", true);
            StartCoroutine(GoBackToSquirming(0.5f));

            _limbsCut++;
        }
    }

    private IEnumerator GoBackToSquirming(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (_limbsCut < 4)
        {
            poorGuy.GetComponent<Animator>().SetBool("severed", false);
        }

        else if (_limbsCut == 4)
        {
            intercom.GetComponent<BoxCollider>().enabled = true;
            intercomCamera.SetActive(true);
        }

    }

}
