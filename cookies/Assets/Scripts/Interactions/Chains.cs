using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chains : Interactable
{
    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    public GameObject player;
    public Text noticeText;
    public GameObject chainsawObj;
    public GameObject carrolBossFightTrigger;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }


    public override void InteractAction()
    {
        chainsawObj.tag = "Interactable";
        carrolBossFightTrigger.SetActive(true);
        Destroy(gameObject);
    }

    public override void FailMessage()
    {
        _notice.ChangeText("BOLT CUTTERS REQUIRED");
    }

}
