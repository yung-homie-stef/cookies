using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public Transform itemsParent;
    public Slot[] slots;
    public GameObject inventoryUI;
    public GameObject player;
    public GameObject VHS_Camera;
    public Image cursorImage; 

    [HideInInspector]
    public bool visible = false;


    Inventory inventory;
    private Player _playerScript;
    protected CameraController _camControlller;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        inventory.onItemChangeCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<Slot>();
        _playerScript = player.GetComponent<Player>();
        _camControlller = VHS_Camera.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.interactionTarget = null;

            if (visible == false)
            {
                EnableUI();
            }
            else
            {
                DisableUI();
            }
        }
    }

    private void UpdateUI()
    {
        for (int i =0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
                slots[i].ClearSlot();
        }
    }

    public void EnableUI()
    {
        inventoryUI.SetActive(true);
        visible = true;
        _playerScript.DisableMovement();
        _camControlller.enabled = false;
        cursorImage.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableUI()
    {
        inventory.interactionTarget = null;
        inventoryUI.SetActive(false);
        visible = false;
        _playerScript.EnableMovement();
        _camControlller.enabled = true;
        cursorImage.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
