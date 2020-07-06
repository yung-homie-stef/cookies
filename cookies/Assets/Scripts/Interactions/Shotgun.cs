using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : Interactable
{
    public GameObject player;
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

    public override void Interact(Player player, string[] tags)
    {
        if (_hasArmed == false)
        {
            for (int j = 0; j < tags.Length; j++)
            {
                if (tags[j] == "Bullets")
                {
                    _hasArmed = true;
                    break;
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!_hasArmed)
        {
            _notice.ChangeText("AMMO REQUIRED");
        }
    }

    public override void InteractAction()
    {
       
    }

    public override void FailMessage()
    {
        _notice.ChangeText("LEAD PIPE REQUIRED");
    }

}
