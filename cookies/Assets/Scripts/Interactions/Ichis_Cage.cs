using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ichis_Cage : Interactable
{
    public GameObject player;
    public Text noticeText;
    public GameObject ichi;

    private Inventory _inventory;
    private Notice _notice;
    private Mr_Lust _mrLustScript;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _mrLustScript = ichi.GetComponent<Mr_Lust>();
    }

    public override void InteractAction()
    {
        _animator.SetBool("is_opened", true);
        _mrLustScript.dialogueValue++;
        gameObject.layer = 0; // no longer interactable
    }

    public override void FailMessage()
    {
        _notice.ChangeText("LOCKPICK REQUIRED", 6.0f);
    }
}
