using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public Transform renderTransform;
    public Vector3 renderScale;

    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public void AddItem()
    {
        for (int i = 0; i < _inventory.UISlots.Length; i++)
        {
            if (_inventory.isFull[i] == false)
            {
                // item can be added
                _inventory.isFull[i] = true;
                gameObject.layer = 9 + i;
                gameObject.transform.position = renderTransform.position;
                gameObject.transform.localScale = renderScale;
                _inventory.inventoryItems[i] = gameObject;
                break;
            }
        }
    }
}
