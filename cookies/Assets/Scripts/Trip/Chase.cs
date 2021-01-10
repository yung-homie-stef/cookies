using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public Transform highwayRestart;
    public GameObject roachTrigger_1;
    public GameObject roachTrigger_2;
    public GameObject roachTroup_1;
    public GameObject roachTroup_2;

    private NavMeshAgent _agent;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        initialPos = gameObject.transform.position;
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
            player.transform.position = highwayRestart.transform.position;
            gameObject.transform.position = initialPos;
            roachTrigger_1.SetActive(true);
            roachTrigger_2.SetActive(true);
            roachTroup_1.SetActive(true);
            roachTroup_2.SetActive(true);
        }
    }
}
