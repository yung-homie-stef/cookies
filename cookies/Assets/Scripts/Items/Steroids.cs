using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steroids : Action
{
    public GameObject player;
    public Text noticeText;
    public string newText;

    private Player _playerScript;
    private Movement _movement;
    private Notice _notice;
    private Inventory _inventory;


    // Start is called before the first frame update
    void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
        _movement = player.GetComponent<Movement>();
        _playerScript = player.GetComponent<Player>();
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use()
    {
        _playerScript.roided = true; // allow player to punch with this bool
        _movement.speed *= 1.5f;
        _notice.ChangeText(newText);
        _inventory.isFull[_inventory.GetCurrentSlot()] = false;
        Destroy(_inventory.inventoryItems[_inventory.GetCurrentSlot()]);
    }
}
