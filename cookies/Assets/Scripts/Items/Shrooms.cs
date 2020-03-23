using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : Action
{
    public GameObject salvador;
    public ParticleSystem shroomSmoke;
    public GameObject player;

    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        shroomSmoke.Play(); // create a puff of smoke for salvador to appear in
        StartCoroutine(Hallucinate(1.0f));
    }

    private IEnumerator Hallucinate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        salvador.SetActive(true);
        _inventory.isFull[_inventory.GetCurrentSlot()] = false;
        Destroy(_inventory.inventoryItems[_inventory.GetCurrentSlot()]);
    }
}
