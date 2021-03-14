using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp_Boat : Interactable
{
    public GameObject actualBoatModel;
    public GameObject boatCollider;
    public Notice _notice;

    private GameObject player;
    private bool hasGas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasGas)
            {
                player = other.gameObject;
                player.transform.parent = actualBoatModel.transform;
            }
            else
            {
                // drive away
                player = other.gameObject;
                player.transform.parent = actualBoatModel.transform;
                _animator.SetBool("leaving", true);
            }

        }
    }

    public void UntetherPlayer()
    {
        player.transform.parent = null;
        boatCollider.SetActive(false);
    }

    public override void InteractAction()
    {
        if (!hasGas)
        {
            hasGas = true;
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText("GASOLINE REQUIRED");
    }
}
