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
    }
}
