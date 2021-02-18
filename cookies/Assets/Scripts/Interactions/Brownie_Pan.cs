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

    private Inventory _inventory;
    private Notice _notice;
    private Animator _ovenAnimator;
    private bool _hasBatter;
    private bool _hasCBD;
    private string _brownieMessage;
    private Interactable _browniePanInteractable;

    // Start is called before the first frame update
    new void Start()
    {
        _brownieMessage = "I NEED BATTER FIRST";
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
                // play oven ding
            }
            else if (_hasBatter && _hasCBD)
            {
                weedBrownie.SetActive(true);
                // play oven ding
            }
            
        }
    }


    public override void InteractAction()
    {
        if (_browniePanInteractable.requiredTags[0] == "Batter")
        {
            _hasBatter = true;
           StartCoroutine(ChangeBrownieRequirements(0.5f));
            
        }
        else if (_browniePanInteractable.requiredTags[0] == "CBD")
        {
            _hasCBD = true;
        }

    }

    private IEnumerator ChangeBrownieRequirements(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _brownieMessage = "HE SAID HE WANTED BROWNIES BAKED WITH CBD";
        reqType = RequirementType.Single;
        requiredTags = new string[1];
        requiredTags[0] = "CBD";
    }

    public override void FailMessage()
    {
        _notice.ChangeText(_brownieMessage);
    }



}
