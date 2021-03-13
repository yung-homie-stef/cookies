using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public Transform highwayRestart;

    public GameObject roachTrigger_1;
    public GameObject roachTrigger_2;
    public Image deathScreenImage;
    public Image rewindIcon;

    public GameObject[] roaches;
    public Transform[] roachPos;
    public Vector3[] initialPos;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        for (int i=0; i < roaches.Length; i++)
        {
            initialPos[i] = roachPos[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DisableDeathScreen(2.0f));
            deathScreenImage.enabled = true;
            rewindIcon.enabled = true;

            player.transform.position = highwayRestart.transform.position;

        }
    }

    private IEnumerator DisableDeathScreen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        deathScreenImage.enabled = false;
        rewindIcon.enabled = false;

        for (int i =0; i < roaches.Length; i++)
            {
                roaches[i].transform.position = initialPos[i];
                roaches[i].SetActive(false);
            }
    }
}
