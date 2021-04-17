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
    public bool isHinting;

    public Text hintText;

    public string hintPhrase;

    private void Start()
    {
        _notice = noticeText.GetComponent<Notice>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _notice.ChangeText(newText, timeAlloted);

        if (isHinting)
        {
            hintText.text += "\n- " + hintPhrase;
        }

        StartCoroutine(SelfDestruct(0.5f));
    }

    private IEnumerator SelfDestruct(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
