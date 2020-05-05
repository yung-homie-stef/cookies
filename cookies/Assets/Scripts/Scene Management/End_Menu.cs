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

    private TextMeshProUGUI threadTitleTMPro;
    private TextMeshProUGUI threadEffectTMPro;
    private TextMeshProUGUI threadHintTMPro;
    private GameObject rotatingModelOnScreen;

    public void Init()
    {
        if (globalEndMenu)
        {
            Debug.Log("beans");
            Destroy(gameObject);
            return;
        }
        globalEndMenu = this;


        if ((threadTitleTMPro = threadTitle.GetComponent<TextMeshProUGUI>())
           && (threadEffectTMPro = threadEffect.GetComponent<TextMeshProUGUI>()) // null check
           && (threadHintTMPro = threadHint.GetComponent<TextMeshProUGUI>()))
        {
            Debug.Log("i think i got it");
        }
        else
        {
            Debug.Log("failed to get a text mesh");
        }
        
    }

    private void Awake()
    {

    }

    public void SetStatusOfThreadCompletion(End_Condition cond)
    {
        Debug.Log(cond.threadName);
        Debug.Log(threadTitleTMPro.name);
        Cursor.lockState = CursorLockMode.None;
        threadTitleTMPro.text = cond.threadName;
        threadEffectTMPro.text = cond.threadEffect;
        threadHintTMPro.text = cond.nextThreadHint;
    }

    public void ReturnToMainMenu()
    {
        Game_Manager.globalGameManager.endScreen.SetActive(false);
        SceneManager.LoadScene(0); // go back to start
    }
}
