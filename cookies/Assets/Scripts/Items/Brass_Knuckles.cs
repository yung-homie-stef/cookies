using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brass_Knuckles : Action
{
    public GameObject playerPalm;
    public Vector3 weaponRotation;
    public Vector3 weaponRepositioning;
    public Text weaponEquipText;

    private bool _wielding;
    private bool _relaxed;
    private GameObject _duplicate;
    private Animator _animator;
    private Vector3 _localScale;

    // Start is called before the first frame update
    void Start()
    {
        _wielding = false;
        _relaxed = true;
        _inventory = player.GetComponent<Inventory>();
        _animator = player.GetComponent<Animator>();
        _localScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!Inventory.instance.inventoryUIScript.visible)
            {
                if (_wielding)
                {
                    if (_relaxed) // to prevent spamming
                        Punch();
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
                Audio_Manager.globalAudioManager.PlaySound("unequip", Audio_Manager.globalAudioManager.intangibleSoundArray);
            }
            else if (_wielding == false)
            {
                _inventory.isWeaponEquipped = true;
                // create a duplicate of the weapon that rests in the player's hand

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
                Audio_Manager.globalAudioManager.PlaySound("equip", Audio_Manager.globalAudioManager.intangibleSoundArray);
            }
        }
    }

    private void Punch()
    {
        _animator.Play("punch"); // play melee animation
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
            _duplicate.GetComponent<BoxCollider>().enabled = true;

        else if (condition == 0)
            _duplicate.GetComponent<BoxCollider>().enabled = false;
    }
}
