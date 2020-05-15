using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Drink : Action
{
    public Text noticeText;
    public string newText;
    public AudioClip drinkSound;

    private Movement _movement;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _movement = player.GetComponent<Movement>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            GetComponent<AudioSource>().PlayOneShot(drinkSound);
            _movement.playerSpeed *= 2; // become faster
            _notice.ChangeText(newText);
        }
    }

}
