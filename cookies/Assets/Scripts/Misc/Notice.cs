﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Notice : MonoBehaviour
{
    public Text HUDText;

    // Start is called before the first frame update
    public void ChangeText(string notice)
    {
        HUDText.text = notice;
        this.gameObject.SetActive(true);
        StartCoroutine(MakeTextDisappear(3.0f));
    }

    private IEnumerator MakeTextDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}