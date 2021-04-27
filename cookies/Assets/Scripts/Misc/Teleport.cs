using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public Transform teleportPoint;
    public Animator blackOut;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            blackOut.SetBool("faded", true);
            StartCoroutine(TeleportPlayer(3.0f));
        }

    }

    private IEnumerator TeleportPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.transform.position = teleportPoint.transform.position;
        blackOut.SetBool("faded", false);
    }
}
