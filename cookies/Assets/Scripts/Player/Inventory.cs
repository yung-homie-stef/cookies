using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangeCallback;
    public GameObject noticeText;
    public Interactable interactionTarget = null;
    public GameObject inventory_UI = null;
    public Inventory_UI inventoryUIScript;

    public GameObject[] inventoryUISlots;

    public bool[] isSlotFull;
    public int space = 10;
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public List<GameObject> playerInventoryItems = new List<GameObject>();

    public GameObject playerInventoryIndicator;
    
    public bool isWeaponEquipped; // variable that dictates whether player can cycle through inventory items

    //[SerializeField]
    public static int currentSelectedSlot = 0;
    private Player player;
    

    private Notice _notice;

    #region SINGLETON
    private void Awake()
    {
        if (instance !=null)
        {
            Debug.LogWarning("more than one instance of inventory found");
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        isWeaponEquipped = false;
        _notice = noticeText.GetComponent<Notice>();
        player = gameObject.GetComponent<Player>();
        inventoryUIScript = inventory_UI.GetComponent<Inventory_UI>();
    }

    private void Update()
    {
       
    }

    private void SelectItemInInventory(int currentSlot)
    {
       
    }

    public int GetCurrentSlot()
    {
        return currentSelectedSlot;
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            _notice.ChangeText("INVENTORY FULL", 6.0f);
            return false;
        }

        items.Add(item);

        if (onItemChangeCallback != null)
        onItemChangeCallback.Invoke();

        return true;
    }

    public void DestroyItem(Item item)
    {
        int i = items.IndexOf(item);
        gameObject.GetComponent<Player>().Drop(i);

        for (int index = i; index < items.Count; index++)
        {
            playerInventoryItems[index].layer--;
        }

        GameObject soon2BeDeleted = playerInventoryItems[i].gameObject;

        playerInventoryItems.RemoveAt(i);
        items.Remove(item);
        Destroy(soon2BeDeleted);

        if (onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }

    }

    public void RemoveItem(Item item)
    {
        int i = items.IndexOf(item);
        gameObject.GetComponent<Player>().Drop(i);

        for (int index = i; index < items.Count; index++)
        {
            playerInventoryItems[index].layer--;
        }

        playerInventoryItems.RemoveAt(i);
        items.Remove(item);

        if (onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }

    }

    public void UseItem(int index)
    {
        player.lastUsedItem = playerInventoryItems[index].GetComponent<AcquirableInteractable>().itemScriptableObj;

        if (interactionTarget == null)
        {
            if (playerInventoryItems[index].GetComponent<Action>())
            {
                playerInventoryItems[index].GetComponent<Action>().Use(index);
            }
        }
        else
        {
            interactionTarget.Interact(player, playerInventoryItems[index].GetComponent<Tags>().tags);
            inventoryUIScript.DisableUI();
        }
    }

    public void DisplayFailedItemUsageText()
    {
        interactionTarget.FailMessage();
    }


}
