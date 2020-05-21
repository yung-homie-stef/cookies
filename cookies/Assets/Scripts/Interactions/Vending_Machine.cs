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
    private bool _hasVended = false;

    // Start is called before the first frame update
    new void Start()
    {
        _hasVended = false;
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }


    public override void InteractAction()
    {
        if (!_hasVended)
        {
            
            StartCoroutine(Vend(4.0f));
            candyBar.GetComponent<BoxCollider>().enabled = true;
            _hasVended = true;
            GetComponent<AudioSource>().PlayOneShot(vendingMachineSound);
            _hasVended = true;
        }
        
    }

    private IEnumerator Vend(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _animator.SetBool("vending", true);
    }

    public override void FailMessage()
    {
        _notice.ChangeText("CURRENCY REQUIRED");
    }
}

                    
    

