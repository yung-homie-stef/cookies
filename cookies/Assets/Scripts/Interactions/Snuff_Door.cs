using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snuff_Door : Interactable
{
    public GameObject player;
    public Text noticeText;
    public int sceneIndex;
    public GameObject blackout;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;
    private bool opened = false;

    // Start is called before the first frame update
    new void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        if (!opened)
        {
            if (_inventory.playerInventoryItems.Count <= 0)
            {
                _animator.SetBool("is_opened", true);
                blackout.GetComponent<Animator>().SetBool("faded", true);
                StartCoroutine(GoToSnuffChamber(5.0f));
                opened = true;
            }
            else
                _notice.ChangeText("Quit fuckin' around, empty your pockets!");
        }
    }

    private IEnumerator GoToSnuffChamber(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }

}
