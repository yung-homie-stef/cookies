using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roofie : Interactable
{
    public GameObject player;
    public GameObject manager;
    public Text noticeText;
    public GameObject blackout;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        blackout.GetComponent<Animator>().SetBool("faded", true);
        StartCoroutine(UnfadeBlack(1.5f));
    }

    public override void FailMessage()
    {
        _notice.ChangeText("THAT WON'T KNOCK HIM OUT");
    }

    private IEnumerator UnfadeBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        blackout.GetComponent<Animator>().SetBool("faded", false);

       manager.GetComponent<FC_Manager>().Drink();

        Destroy(gameObject);
    }
}
