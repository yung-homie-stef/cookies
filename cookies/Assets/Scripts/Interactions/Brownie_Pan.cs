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
    private Tags _tags;
    private Notice _notice;
    private Animator _ovenAnimator;
    private bool _hasBatter;
    private bool _hasCBD;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _ovenAnimator = ovenDoor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasBatter)
        {
            if (_ovenAnimator.GetBool("is_opened") == false && _hasCBD == false)
            {
                StartCoroutine(Bake(1.0f, false));
            }
            else if (_ovenAnimator.GetBool("is_opened") == false && _hasCBD)
            {
                StartCoroutine(Bake(1.0f, true));
            }
        }
    }

    public override void Interact()
    {
        for (int i = 0; i < _inventory.UISlots.Length; i++)
        {
            if (_inventory.inventoryItems[i] != null)
            {
            _tags = _inventory.inventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Batter") // adding brownie mix
                    {
                        _hasBatter = true;
                        _inventory.isFull[i] = false;
                        Destroy(_inventory.inventoryItems[i]);
                        _notice.ChangeText("CLOSE OVEN TO BAKE");
                        break;
                    }
                    else if (_tags.tags[j] == "CBD") // adding cannabinoids 
                    {
                        _hasCBD = true;
                        _inventory.isFull[i] = false;
                        Destroy(_inventory.inventoryItems[i]);
                        _notice.ChangeText("CBD ADDED TO RECIPE");
                        break;
                    }
                }
            }    
        }
        if (_hasBatter == false && _hasCBD == false)
        {
            _notice.ChangeText("BROWNIE MIX REQUIRED");
        }
    }

    private IEnumerator Bake(float waitTime, bool isWeedBrownie)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<Collider>().enabled = false;
        if (isWeedBrownie)
            weedBrownie.SetActive(true);
        else
            brownie.SetActive(true);
    }
}
