using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soviet_Bouncer : Interactable
{
    public GameObject player;
    public GameObject sovietDoor;
    public GameObject blackout;
    public GameObject blackoutCanvas;
    public Text noticeText;

    private string[] currentSentences;
    private Dialogue _dialogue;
    private bool eventHappensWhenTalkingIsDone;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
      
        blackoutCanvas.SetActive(true);
        blackout.GetComponent<Animator>().SetBool("faded", true);
        StartCoroutine(UnfadeBlack(1.5f));
    }

    public override void FailMessage()
    {
        _notice.ChangeText("If you don't know, you don't know амеры.");
    }

    private IEnumerator UnfadeBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        blackout.GetComponent<Animator>().SetBool("faded", false);
        sovietDoor.GetComponent<Animator>().SetBool("is_opened", true);
        Destroy(gameObject);
    }

}


