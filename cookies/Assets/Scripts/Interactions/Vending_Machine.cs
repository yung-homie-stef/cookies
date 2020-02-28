using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vending_Machine : Interactable
{
    public GameObject candyBar;
    public GameObject player;

    private Animator _animator;
    private Inventory _inventory;
    private Tags _tags;
    private bool _hasVended;

    // Start is called before the first frame update
    void Start()
    {
        _hasVended = false;
        _animator = gameObject.GetComponent<Animator>();
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Interact()
    {
        if (_hasVended == false)
        {
            for (int i = 0; i < _inventory.UISlots.Length; i++)
            {
                _tags = _inventory.inventoryItems[i].GetComponent<Tags>();

                for (int j = 0; j < _tags.tags.Length; j++)
                {
                    if (_tags.tags[j] == "Currency")
                    {
                        _animator.SetBool("vending", true);
                        candyBar.GetComponent<BoxCollider>().enabled = true;
                        _hasVended = true;
                        _inventory.isFull[i] = false;
                        Destroy(_inventory.inventoryItems[i]);
                        break;
                    }
                }
            }
        }
    }
}
                    
    

