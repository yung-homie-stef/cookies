using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brownie_Pan : Interactable
{
    public GameObject player;
    public GameObject brownie;
    public GameObject weedBrownie;
    public GameObject ovenDoor;
    public Text noticeText;
    public AudioClip oven_ding;
    public bool swamped = false;

    private Inventory _inventory;
    private Notice _notice;
    private Animator _ovenAnimator;
    private bool _hasBatter;
    private bool _hasCBD;
    private bool _hasBaked;
    public string _brownieMessage;
    private Interactable _browniePanInteractable;

    // Start is called before the first frame update
    new void Start()
    {
        _brownieMessage = "I NEED BROWNIE MIX FIRST";
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _ovenAnimator = ovenDoor.GetComponent<Animator>();
        _browniePanInteractable = gameObject.GetComponent<Interactable>();
    }

    private void Update()
    {
        if (_ovenAnimator.GetBool("is_opened") == false)
        {
            if (_hasBatter)
            {
                if (_hasCBD)
                {
                    weedBrownie.SetActive(true);

                    if (!_hasBaked)
                    {
                        StartCoroutine(PlayOvenDing(1.0f));
                        _hasBaked = true;
                    }
                }
                else
                {
                    brownie.SetActive(true);

                    if (!_hasBaked)
                    {
                        StartCoroutine(PlayOvenDing(1.0f));
                        _hasBaked = true;
                    }
                }

               
            }
            else if (_hasCBD)
            {
                weedBrownie.SetActive(true);

                if (!_hasBaked)
                {
                    StartCoroutine(PlayOvenDing(1.0f));
                    _hasBaked = true;
                }
            }
            
        }
    }


    public override void InteractAction()
    {
        if (_browniePanInteractable.requiredTags[0] == "Batter")
        {
            _hasBatter = true;
            _hasBaked = false;
              
        }
        else if (_browniePanInteractable.requiredTags[0] == "CBD")
        {
            _hasCBD = true;
            _hasBaked = false;
        }

    }


    public override void FailMessage()
    {
        _notice.ChangeText(_brownieMessage, 6.0f);
    }

    private IEnumerator PlayOvenDing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<AudioSource>().PlayOneShot(oven_ding);
    }

}
