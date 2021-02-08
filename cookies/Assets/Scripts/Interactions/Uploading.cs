using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uploading : Interactable
{
    public GameObject player;
    public GameObject loadingBar;
    public GameObject snuffSite;
    public Text noticeText;
    public End_Condition a_floridian_film_Thread;
    public GameObject blackOut;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    new void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void InteractAction()
    {
        loadingBar.SetActive(true);

        StartCoroutine(UploadSnuffFilm(7.0f));
        GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator UploadSnuffFilm(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        loadingBar.SetActive(false);
        snuffSite.SetActive(true);
        StartCoroutine(CompleteRandysThread(5.0f));
        blackOut.GetComponent<Animator>().SetBool("faded", true);
    }

    private IEnumerator CompleteRandysThread(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(a_floridian_film_Thread);
    }

    public override void FailMessage()
    {
        _notice.ChangeText("CD REQUIRED");
    }
}
