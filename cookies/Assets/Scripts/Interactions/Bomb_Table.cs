using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb_Table : Interactable
{
    public GameObject pipeBomb;
    public GameObject player;
    public Text noticeText;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }


    public override void InteractAction()
    {
        pipeBomb.SetActive(true);
        
    }

    public override void FailMessage()
    {
        _notice.ChangeText("LEAD PIPE REQUIRED", 6.0f);
    }


}
