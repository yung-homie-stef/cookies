using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : Interactable
{
    public GameObject player;
    public GameObject button;
    public Text noticeText;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;
    private bool _hasArmed;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _hasArmed = false;
    }

    public override void InteractAction()
    {
        if (!_hasArmed)
        {
            button.GetComponent<BoxCollider>().enabled = true;
            _hasArmed = true;
        }

    }

    public override void FailMessage()
    {
        _notice.ChangeText("AMMO REQUIRED", 6.0f);
    }

}
