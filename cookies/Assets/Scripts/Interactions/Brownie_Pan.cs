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
    private string _brownieMessage;
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
            if (_hasBatter && !_hasCBD)
            {
                brownie.SetActive(true);

                if (!_hasBaked)
                {
                    StartCoroutine(PlayOvenDing(1.0f));
                    _hasBaked = true;
                }
            }
            else if (_hasBatter && _hasCBD)
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
              
        }
        else if (_browniePanInteractable.requiredTags[0] == "CBD" && swamped)
        {
            _hasCBD = true;
            ChangeBrownieRequirements(0.5f);
        }

    }

    private IEnumerator ChangeBrownieRequirements(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _brownieMessage = "JUST NEED THE BROWNIE MIX NOW";
        reqType = RequirementType.Single;
        requiredTags = new string[1];
        requiredTags[0] = "Batter";
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
