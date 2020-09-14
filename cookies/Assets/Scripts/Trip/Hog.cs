using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : Victim
{
    public static int hogsLeft = 12;
    public GameObject cop_ensemble;
    public Transform teleportPoint;
    public GameObject player;

    public override void Die()
    {
        base.Die();

        if (hogsLeft > 0)
        {
            //hogsLeft--;
        }
        else
        {
            //StartCoroutine(RemoveCops(5.0f));
        }
    }

    private IEnumerator RemoveCops(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        player.transform.position = teleportPoint.transform.position;
        cop_ensemble.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            hogsLeft--;
            Debug.Log(hogsLeft);

            if (hogsLeft == 0)
            {
                player.transform.position = teleportPoint.transform.position;
                cop_ensemble.SetActive(false);
            }
        }
    }


}
