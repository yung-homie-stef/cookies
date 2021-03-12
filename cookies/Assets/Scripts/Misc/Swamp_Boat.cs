using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp_Boat : MonoBehaviour
{
    public GameObject actualBoatModel;
    public MeshCollider boatCollider;

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            player.transform.parent = actualBoatModel.transform;
        }
    }

    public void UntetherPlayer()
    {
        player.transform.parent = null;
        boatCollider.enabled = false;
    }
}
