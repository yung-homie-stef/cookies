﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randy : Interactable
{
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public GameObject dialogueManager;
    public GameObject CD;
    public bool isScared;

    [SerializeField]
    private string[] currentSentences;
    private Dialogue _dialogue;
    private GameObject _contactPoint;

    new void Start()
    {
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        SetScaredStatus(isScared);
    }

    public override void InteractAction()
    {
            HandleDialogue(0);
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject, true);
    }

    private string[] UpdateDialogue(string[] lines)
    {
        string[] sentences = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            sentences[i] = lines[i];
        }
        return sentences;
    }

    void SetScaredStatus(bool status)
    {
        if (status == true)
        {
            GetComponent<Animator>().SetBool("is_scared", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            _contactPoint = other.gameObject;
            GetComponent<Victim>().TakeDamage("melee", _contactPoint.transform.position, _contactPoint.transform.forward);

            if (GetComponent<Victim>().hitPoints == 0)
            {
                if (isScared)
                {
                    GetComponent<Animator>().enabled = false;
                    StartCoroutine(GiveCD(1.5f));
                    

                }
            }
        }
    }

    private IEnumerator GiveCD(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CD.SetActive(true);
        CD.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
        CD.tag = "Interactable";
    }

   
}
