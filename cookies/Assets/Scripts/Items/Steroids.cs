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


    // Start is called before the first frame update
    void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
        _movement = player.GetComponent<Movement>();
        _playerScript = player.GetComponent<Player>();
    }

    public override void Use()
    {
        _playerScript.roided = true; // allow player to punch with this bool
        _movement.speed *= 1.5f;
        _notice.ChangeText(newText);
        Destroy(this);
    }
}
