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
 

    public int space = 10;
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public List<GameObject> playerInventoryItems = new List<GameObject>();

    public GameObject playerInventoryIndicator;
    public GameObject[] inventoryUISlots;
    
    public bool[] isSlotFull;
    public bool isWeaponEquipped; // variable that dictates whether player can cycle through inventory items

    //[SerializeField]
    public static int currentSelectedSlot = 0;
    private Player player;
    private Inventory_UI _inventoryUIScript;

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
        _inventoryUIScript = inventory_UI.GetComponent<Inventory_UI>();
    }

    private void Update()
    {
        #region OLD CODE
        if (isWeaponEquipped == false) // when an object that takes up the player's hand is used, prohibit them from being able to select and use other items
        {
            if (Input.GetButton("FirstSlot"))
            {
                currentSelectedSlot = 0;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("SecondSlot"))
            {
                currentSelectedSlot = 1;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("ThirdSlot"))
            {
                currentSelectedSlot = 2;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("FourthSlot"))
            {
                currentSelectedSlot = 3;
                SelectItemInInventory(currentSelectedSlot);
            }
            else if (Input.GetButton("FifthSlot"))
            {
                currentSelectedSlot = 4;
                SelectItemInInventory(currentSelectedSlot);
            }
        }
        #endregion
    }

    private void SelectItemInInventory(int currentSlot)
    {
        #region OLD CODE
        //// only allow a slot to be selectable if its full
        //if (isSlotFull[currentSlot])
        //{
        //    if (playerInventoryIndicator.activeSelf == false)
        //    {
        //        playerInventoryIndicator.SetActive(true);
        //    }

        //    Vector3 newPosition = new Vector3(playerInventoryIndicator.transform.position.x, inventoryUISlots[currentSlot].transform.position.y, 0);
        //    playerInventoryIndicator.transform.position = newPosition;
        //}
        #endregion
    }

    public int GetCurrentSlot()
    {
        return currentSelectedSlot;
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            _notice.ChangeText("INVENTORY FULL");
            return false;
        }

        items.Add(item);

        if (onItemChangeCallback != null)
        onItemChangeCallback.Invoke();

        return true;
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
            onItemChangeCallback.Invoke();
       
    }

    public void UseItem(int index)
    {

        if (interactionTarget == null)
        {
            playerInventoryItems[index].GetComponent<Action>().Use(index);
        }
        else
        {
            interactionTarget.Interact(player, playerInventoryItems[index].GetComponent<Tags>().tags);
            _inventoryUIScript.DisableUI();
        }
    }


}
