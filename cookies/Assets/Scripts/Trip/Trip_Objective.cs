using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trip_Objective : Interactable
{
    public GameObject player;
    public Transform teleportPoint;
    public GameObject[] disabledParts;
    public GameObject[] enabledParts;
    public GameObject transitionCanvas;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;
    private Movement _movement;

    // Start is called before the first frame update
    new void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _movement = player.GetComponent<Movement>();
    }

    public override void InteractAction()
    {
        player.transform.position = teleportPoint.transform.position;
        transitionCanvas.SetActive(true);
        StartCoroutine(MovePlayer(1.0f));
        _movement.enabled = false;
    }   

    private IEnumerator MovePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        transitionCanvas.SetActive(false);
        _movement.enabled = true;

        for (int i = 0; i < disabledParts.Length; i++)
        {
            disabledParts[i].SetActive(false);
        }

        for (int i = 0; i < enabledParts.Length; i++)
        {
            enabledParts[i].SetActive(true);
        }

    }

}
