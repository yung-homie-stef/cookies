using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Father_Huxley : Interactable
{
    public int animationInt;
    public GameObject dialogueManager;
    public Set_of_Sentences[] sentenceSets;
    public GameObject player;
    public Text noticeText;

    [SerializeField]
    private int _dialogueValue;
    private string[] currentSentences;
    private Dialogue _dialogue;
    private Tags _tags;
    private Animator _animator;
    private Inventory _inventory;
    private bool _hasPaid;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _hasPaid = false;
        _dialogue = dialogueManager.GetComponent<Dialogue>();
        _dialogueValue = 0;
        _animator = GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
        _animator.SetInteger("praise_animation", animationInt);
    }

    public override void Interact()
    {
        //if (!_hasPaid)
        //{
        //    if (CheckForTithes() == true)
        //    {
        //        HandleDialogue(_dialogueValue); // if player has money for tithes, pay father huxley and THEN advance dialogue
        //        _hasPaid = true;
        //    }
        //}
        //else
        {
            HandleDialogue(_dialogueValue);
        }
    }

    public void ConfirmTaskCompleted()
    {
        _dialogueValue++;
    }

    private void HandleDialogue(int setIndex)
    {
        currentSentences = sentenceSets[setIndex].sentences;
        _dialogue.BeginDialogue(UpdateDialogue(currentSentences), gameObject);
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

    private bool CheckForTithes()
    {

        for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        {
            if (_inventory.playerInventoryItems[i] != null)
            {
                _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Currency")
                    {
                        _inventory.isSlotFull[i] = false;
                        Destroy(_inventory.playerInventoryItems[i]);
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
