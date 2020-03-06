using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Action
{
    public GameObject player;
    public GameObject playerPalm;

    private bool _cocked;
    private Vector3 _localScale;
    private Inventory _inventory;
    private GameObject _duplicate;

    // Start is called before the first frame update
    void Start()
    {
        _cocked = false; // whether the gun is out or not
        _inventory = player.GetComponent<Inventory>();
        _localScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        if (_cocked == true)
        {
            _inventory.canSelect = true;
            Destroy(_duplicate);
            _cocked = false;
            
        }
        else if (_cocked == false)
        {
            _inventory.canSelect = false;
            _duplicate = Instantiate(gameObject, playerPalm.transform.position, player.transform.rotation);
            _duplicate.transform.Rotate(0,90,90);
            _duplicate.transform.localScale = _localScale;
            _duplicate.layer = 0;
            _duplicate.transform.parent = playerPalm.transform; // make the gun a child of the palm
            _cocked = true;
        }
    }
}
