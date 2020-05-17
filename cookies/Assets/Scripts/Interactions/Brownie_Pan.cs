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

    public override void InteractAction()
    {
        _notice.ChangeText("CLOSE OVEN TO GET BAKED");
    }

    public override void Interact(Player player, string[] tags)
    {
        base.Interact(player, tags);

        for (int j = 0; j < tags.Length; j++)
        {
            if (tags[j] == "Batter") // adding brownie mix
            {
                _hasBatter = true;
                _notice.ChangeText("CLOSE OVEN TO BAKE");
                break;
            }
            else if (tags[j] == "CBD") // adding cannabinoids 
            {
                _hasCBD = true;
                _notice.ChangeText("CBD ADDED TO RECIPE");
                break;
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!_hasBatter)
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
