using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Action
{
    public GameObject player;
    public GameObject playerPalm;
    public float range;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    private bool _cocked;
    private bool _reloaded;
    private Vector3 _localScale;
    private Inventory _inventory;
    private GameObject _duplicate;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _cocked = false; // whether the gun is out or not
        _reloaded = true;
        _inventory = player.GetComponent<Inventory>();
        _animator = player.GetComponent<Animator>();
        _localScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_cocked)
            {
                _animator.Play("shooting");
                if (_reloaded) // to prevent spamming
                Shoot();
            }
        }
    }

    public override void Use()
    {
        if (_cocked == true)
        {
            _inventory.weaponEquipped = false;
            Destroy(_duplicate);
            _cocked = false;
            
        }
        else if (_cocked == false)
        {
            _inventory.weaponEquipped = true;
            // create a duplicate of the gun that rests in the player's hand
            _duplicate = Instantiate(gameObject, playerPalm.transform.position, player.transform.rotation);
            _duplicate.transform.Rotate(0,90,90); 
            _duplicate.transform.localScale = _localScale;
            _duplicate.layer = 0;
            _duplicate.transform.parent = playerPalm.transform; // make the gun a child of the palm
            _cocked = true;
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
       
        RaycastHit _hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out _hit, range))
        {
            if (_hit.transform.GetComponent<Victim>())
            {
                Victim _victim = _hit.transform.GetComponent<Victim>();
                _victim.Die(); // if it bleeds... we can kill it
            }
        }


        _reloaded = false;
        StartCoroutine(Reload(1.0f));

    }

    private IEnumerator Reload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _reloaded = true;
    }
}
