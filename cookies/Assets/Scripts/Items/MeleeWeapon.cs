using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeWeapon : Action
{
    public GameObject playerPalm;
    public Vector3 weaponRotation;
    public Vector3 weaponRepositioning;
    public Text weaponEquipText;
    public string animationName;

    private bool _wielding;
    private bool _relaxed;
    private GameObject _duplicate;
    private Animator _animator;
    private Vector3 _localScale;

    private Player _playerScript;

    // Start is called before the first frame update
    void Start()
    {
        _wielding = false;
        _relaxed = true;
        _inventory = player.GetComponent<Inventory>();
        _animator = player.GetComponent<Animator>();
        _localScale = gameObject.transform.localScale;
        _playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!Inventory.instance.inventoryUIScript.visible)
            {
                if (_wielding)
                {
                    if (_relaxed) // to prevent spamming
                        Swing();
                }
            }
        }

    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            if (_wielding == true)
            {
                _inventory.isWeaponEquipped = false; // put weapon away
                Destroy(_duplicate);
                _wielding = false;

                weaponEquipText.text = "";
                weaponEquipText.enabled = false;
                _inventory.inventoryUIScript.slots[itemIndex].removeButton.interactable = true;
               _playerScript.equipped = false;
                Audio_Manager.globalAudioManager.PlaySound("unequip", Audio_Manager.globalAudioManager.intangibleSoundArray);

                
            }
            else if (_wielding == false)
            {
                if (!_playerScript.equipped)
                {
                    _inventory.isWeaponEquipped = true;
                    // create a duplicate of the weapon that rests in the player's hand

                    _playerScript.SetNumberOfEquippedMeleeItem(itemIndex);

                    _duplicate = Instantiate(gameObject, playerPalm.transform.position, player.transform.rotation);
                    _duplicate.GetComponent<BoxCollider>().enabled = false;
                    _duplicate.gameObject.tag = "Hitbox";
                    _duplicate.transform.Rotate(weaponRotation);
                    _duplicate.transform.localScale = _localScale;
                    _duplicate.layer = 0;
                    _duplicate.transform.parent = playerPalm.transform; // make the weapon a child of the palm
                    _duplicate.transform.localPosition = new Vector3(weaponRepositioning.x, weaponRepositioning.y, weaponRepositioning.z);
                    _wielding = true;

                    weaponEquipText.text = GetComponent<AcquirableInteractable>().itemScriptableObj.itemName + " EQUIPPED";
                    weaponEquipText.enabled = true;

                    _inventory.inventoryUIScript.slots[itemIndex].removeButton.interactable = false;
                    _playerScript.equipped = true;

                    Audio_Manager.globalAudioManager.PlaySound("equip", Audio_Manager.globalAudioManager.intangibleSoundArray);
                }
            }
        }
    }

    private void Swing()
    {
        _animator.Play(animationName); // play melee animation
        _relaxed = false;
        StartCoroutine(RelaxArm(1.0f));
    }

    private IEnumerator RelaxArm(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _relaxed = true;
    }

    public void EnableMeleeHitbox(int condition)
    {
        if (condition == 1)
            _duplicate.transform.GetChild(0).gameObject.SetActive(true);

        else if (condition == 0)
            _duplicate.transform.GetChild(0).gameObject.SetActive(false);
    }

}
