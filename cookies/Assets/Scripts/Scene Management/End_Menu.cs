using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_Menu : MonoBehaviour
{
    public static End_Menu globalEndMenu = null;
    public GameObject threadTitle;
    public GameObject threadEffect;
    public GameObject threadHint;
    public Transform threadModelTransform;

    private TextMeshProUGUI threadTitleTMPro;
    private TextMeshProUGUI threadEffectTMPro;
    private TextMeshProUGUI threadHintTMPro;
    private GameObject rotatingModelOnScreen;

    private void Start()
    {
        if ((threadTitleTMPro = threadTitle.GetComponent<TextMeshProUGUI>())
           && (threadEffectTMPro = threadEffect.GetComponent<TextMeshProUGUI>()) // null check
           && (threadHintTMPro = threadHint.GetComponent<TextMeshProUGUI>()))
        {

        }
        else
        {
            Debug.Log("failed to get a text mesh");
        }
        
    }

    private void Awake()
    {
        globalEndMenu = this;
    }

    public void SetStatusOfThreadCompletion(End_Condition cond)
    {
        threadTitleTMPro.text = cond.threadName;
        threadEffectTMPro.text = cond.threadEffect;
        threadHintTMPro.text = cond.nextThreadHint;
        rotatingModelOnScreen = Instantiate(cond.rotatingModel, threadModelTransform.position, threadModelTransform.rotation);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); // go back to start
    }
}
