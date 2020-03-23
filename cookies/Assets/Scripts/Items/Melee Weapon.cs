using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Action
{
    public GameObject player;
    public GameObject playerPalm;
    public GameObject hitbox;

    private bool _wielding;
    private bool _relaxed;
    private GameObject _duplicate;
    private Inventory _inventory;
    private Animator _animator;
    private Vector3 _localScale;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _animator = player.GetComponent<Animator>();
        _localScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_wielding)
            {
                if (_relaxed)
                Swing();
            }
        }
    }

    public override void Use()
    {
        if (_wielding == true)
        {
            _inventory.weaponEquipped = false; // put weapon away
            Destroy(_duplicate);
            _wielding = false;
        }
        else if (_wielding == false)
        {
            _inventory.weaponEquipped = true;
            // create a duplicate of the gun that rests in the player's hand
            _duplicate = Instantiate(gameObject, playerPalm.transform.position, player.transform.rotation);
            //_duplicate.transform.Rotate(0, 90, 90);
            _duplicate.transform.localScale = _localScale;
            _duplicate.layer = 0;
            _duplicate.transform.parent = playerPalm.transform; // make the gun a child of the palm
            _wielding = true;
        }
    }

    private void Swing()
    {
        _animator.Play("melee"); // play melee animation
        _relaxed = false;
        StartCoroutine(RelaxArm(1.0f));
    }

    private IEnumerator RelaxArm(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _relaxed = true;
    }

    public void EnableMeleeHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableMeleeHitbox()
    {
        hitbox.SetActive(false);
    }
}
