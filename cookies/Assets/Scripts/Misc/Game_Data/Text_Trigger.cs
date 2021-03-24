using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Trigger : MonoBehaviour
{
    public Text noticeText;
    public float timeAlloted;

    public string newText;
    private Notice _notice;

    private void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _notice.ChangeText(newText, timeAlloted);
        StartCoroutine(SelfDestruct(0.5f));
    }

    private IEnumerator SelfDestruct(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
