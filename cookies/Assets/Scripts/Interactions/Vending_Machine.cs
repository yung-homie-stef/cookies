using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vending_Machine : Interactable
{
    public GameObject candyBar;
    public GameObject player;
    public Text noticeText;
    public AudioClip vendingMachineSound;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;
    private bool _hasVended;

    // Start is called before the first frame update
    new void Start()
    {
        _hasVended = false;
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void Interact(Player player, string[] tags)
    {
        if (_hasVended == false)
        {
            for (int j = 0; j < tags.Length; j++)
            {
                if (tags[j] == "Currency") // adding brownie mix
                {
                    _hasVended = true;
                    break;
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!_hasVended)
        {
            _notice.ChangeText("CURRENCY REQUIRED");
        }
    }

    public override void InteractAction()
    {
            StartCoroutine(Vend(4.0f));
            candyBar.GetComponent<BoxCollider>().enabled = true;
            _hasVended = true;
            GetComponent<AudioSource>().PlayOneShot(vendingMachineSound);
        
    }

    private IEnumerator Vend(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _animator.SetBool("vending", true);
    }
}

                    
    

