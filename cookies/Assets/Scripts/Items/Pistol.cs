using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pistol : Action
{
    public GameObject playerPalm;
    public float range;
    public Camera fpsCamera;
    public AudioClip[] gunshotSFX;
    public AudioMixerGroup sfxMixer;
    public Text weaponEquipText;
    public string gunType;
    public string gunName;
    public Vector3 cloneRotation;

    private bool _cocked;
    private bool _reloaded;
    private Vector3 _localScale;
    private Vector3 _bulletPoint;
    private Vector3 _bulletDirection;
    private GameObject _duplicate;
    private Animator _animator;
    private RaycastHit _killedPerson;

    private Player _playerScript;

    // Start is called before the first frame update
    void Start()
    {
        _cocked = false; // whether the gun is out or not
        _reloaded = true;
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
                if (_cocked)
                {
                    if (_reloaded) // to prevent spamming
                        Shoot();
                }
            }
        }  
    }

    public override void Use(int itemIndex)
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            if (_cocked == true)
            {
                _cocked = false;
                _inventory.isWeaponEquipped = false; // put gun away
                Destroy(_duplicate);

                weaponEquipText.text = "";
                weaponEquipText.enabled = false;
                _inventory.inventoryUIScript.slots[itemIndex].removeButton.interactable = true;
                _playerScript.equipped = false;
                Audio_Manager.globalAudioManager.PlaySound("unequip", Audio_Manager.globalAudioManager.intangibleSoundArray);
               

            }
            else if (_cocked == false)
            {
                _inventory.isWeaponEquipped = true;
                // create a duplicate of the gun that rests in the player's hand

                _duplicate = Instantiate(gameObject, playerPalm.transform.position, player.transform.rotation);
                _duplicate.AddComponent<AudioSource>();
                _duplicate.GetComponent<AudioSource>().outputAudioMixerGroup = sfxMixer;

                //_duplicate.transform.Rotate(0, 90, 90);
                _duplicate.transform.Rotate(cloneRotation);
                _duplicate.transform.localScale = _localScale;
                _duplicate.layer = 0;
                _duplicate.transform.parent = playerPalm.transform; // make the gun a child of the palm
                _cocked = true;

                weaponEquipText.enabled = true;
                weaponEquipText.text = gunName + " EQUIPPED";
                _inventory.inventoryUIScript.slots[itemIndex].removeButton.interactable = false;
                _playerScript.equipped = true;
                Audio_Manager.globalAudioManager.PlaySound("equip", Audio_Manager.globalAudioManager.intangibleSoundArray);
            }
        }
    }

    private void Shoot()
    {
        _animator.Play("shooting");
        StartCoroutine(MuzzleFlash(0.7f)); // delay muzzle flash particle effect
        _bulletDirection = fpsCamera.transform.forward;

        RaycastHit _hit;
        if (Physics.Raycast(fpsCamera.transform.position, _bulletDirection, out _hit, range))
        {
            if (_hit.transform.GetComponent<Victim>())
            {
                _killedPerson = _hit;
                _bulletPoint = _hit.point;
                
                Debug.Log(_hit.point);
                StartCoroutine(Kill(0.7f));
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

    private IEnumerator MuzzleFlash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int randomGunSound = Random.Range(0, gunshotSFX.Length);
        PlayGunshotNoise(randomGunSound);
        ParticleSystem muzzleFlash = _duplicate.GetComponentInChildren<ParticleSystem>();
        muzzleFlash.Play();
    }

    void PlayGunshotNoise(int SFX)
    {
        _duplicate.GetComponent<AudioSource>().PlayOneShot(gunshotSFX[SFX]);
    }

    private IEnumerator Kill(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Victim _victim = _killedPerson.transform.GetComponent<Victim>();
        _victim.TakeDamage(gunType, 1, _bulletPoint, _bulletDirection); // if it bleeds... we can kill it
    }
}
