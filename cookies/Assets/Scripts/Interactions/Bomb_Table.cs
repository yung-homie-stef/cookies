using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb_Table : Interactable
{
    public GameObject player;
    public Text noticeText;

    private Inventory _inventory;
    private Tags _tags;
    private Notice _notice;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _notice = noticeText.GetComponent<Notice>();
    }

    public override void Interact()
    {
        if (CheckIfArrayEmpty(_inventory.inventoryItems))
        { 
            _notice.ChangeText("LEAD PIPE REQUIRED");
            return;
        }

        for (int i = 0; i < _inventory.UISlots.Length; i++)
        {

            _tags = _inventory.inventoryItems[i].GetComponent<Tags>();

            if (!_tags)
            {
                Debug.Log("failed to get tag");
                return;
            }

            bool hasLeadPipe = false;
            for (int j = 0; j < _tags.tags.Length; j++)
            {
                if (_tags.tags[j] == "Pipe")
                {
                    hasLeadPipe = true;
                    // create pipe bomb
                    break;
                }
              
            }
            if (!hasLeadPipe)
                _notice.ChangeText("LEAD PIPE REQUIRED");

        }
    }

    public bool CheckIfArrayEmpty<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
                return false;
        }

        return true;
    }
}
