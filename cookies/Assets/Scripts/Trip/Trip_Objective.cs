using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trip_Objective : Interactable
{
    public GameObject player;
    public Transform teleportPoint;
    public GameObject[] disabledParts;
    public GameObject enabledPart;

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
        StartCoroutine(MovePlayer(3.0f));
        player.transform.position = teleportPoint.transform.position;
        _movement.enabled = false;

        for (int i = 0; i < disabledParts.Length; i++)
        {
            disabledParts[i].SetActive(false);
        }

    }

    private IEnumerator MovePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _movement.enabled = true;
        enabledPart.SetActive(true);

    }

}
