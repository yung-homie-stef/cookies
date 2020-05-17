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
    private bool hasCrafted;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        hasCrafted = false;
    }

    public override void Interact(Player player, string[] tags)
    {
        if (hasCrafted == false)
        {
            for (int j = 0; j < tags.Length; j++)
            {
                if (tags[j] == "Lead_Pipe") 
                {
                    hasCrafted = true;
                    break;
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!hasCrafted)
        {
            _notice.ChangeText("LEAD PIPE REQUIRED");
        }
    }

    public override void InteractAction()
    {
        pipeBomb.SetActive(true);
        Audio_Manager.globalAudioManager.PlaySound("ping", Audio_Manager.globalAudioManager.intangibleSoundArray);
    }


}
