using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public Transform renderTransform;
    public Vector3 renderScale;
    public Vector3 renderRotation;
    public float extraX;

    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
    }

    public void AddItem(Item item)
    {
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
        {
            gameObject.layer = Inventory.instance.items.Count + 8;
            gameObject.transform.position = new Vector3(renderTransform.position.x + extraX, renderTransform.position.y, renderTransform.position.z);
            gameObject.transform.localScale = renderScale;
            gameObject.transform.eulerAngles = renderRotation;

            if (Inventory.instance.items.Count <= 10)
            {
                _inventory.playerInventoryItems.Add(gameObject);
            }
        }

        //if (wasPickedup)
        //    Destroy(gameObject);

        #region OLD CODE
        //for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
        //{
        //    if (_inventory.isSlotFull[i] == false)
        //    {

        //        // item can be added
        //        _inventory.isSlotFull[i] = true;
        //        
        //        gameObject.transform.position = new Vector3(renderTransform.position.x + extraX, renderTransform.position.y, renderTransform.position.z);
        //        gameObject.transform.localScale = renderScale;
        //        gameObject.transform.eulerAngles = renderRotation;
        //        
        //        break;
        //    }


        //}
        #endregion

    }
}
