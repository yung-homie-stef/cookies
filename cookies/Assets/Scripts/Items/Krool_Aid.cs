using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krool_Aid : Action
{
    public GameObject galaxyGroup;
    public GameObject blackout;
    public Transform galaxyTransform;
    public GameObject teleportParticle;
    public GameObject Samet;
    public GameObject UFO;
    [HideInInspector]
    public Vector3 preTeleportPosition;

    private Animator _animator;
    private Tags _tags;
    private bool hasTeleported;
    private Samet _sametScript;

    // Start is called before the first frame update
    void Start()
    {
        hasTeleported = false;
        _animator = blackout.GetComponent<Animator>();
        _sametScript = Samet.GetComponent<Samet>();
        _inventory = player.GetComponent<Inventory>();
    }

    public override void Use()
    {
        if (GetComponent<AcquirableInteractable>().canNowUse)
        {
            if (!hasTeleported)
            {
                for (int i = 0; i < _inventory.inventoryUISlots.Length; i++)
                {
                    if (_inventory.playerInventoryItems[i] != null)
                    {
                        _tags = _inventory.playerInventoryItems[i].GetComponent<Tags>();

                        for (int j = 0; j < _tags.tags.Length; j++)
                        {
                            if (_tags.tags[j] == "Rat_Posion")
                            {

                                _sametScript.hasTranslated = true;


                                _inventory.isSlotFull[i] = false;
                                Destroy(_inventory.playerInventoryItems[i]);
                                break;
                            }
                        }
                    }
                }

                StartCoroutine(DrugsKickIn(5.0f));
                _animator.SetBool("faded", true);
            }
        }

        preTeleportPosition = player.transform.position;
    }

    private IEnumerator DrugsKickIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // teleport player to galaxy 
        player.transform.position = galaxyTransform.position;
        player.transform.rotation = Quaternion.Euler(0, -180, 0);

        // stops player from teleporting to space if they are alreasy in space
        hasTeleported = true;
        _animator.SetBool("faded", false);

        
        // make galaxy and all its children visible by placing it on default layer
        foreach (Transform tf in galaxyGroup.transform)
        {
            tf.gameObject.layer = 0;
        }

        teleportParticle.SetActive(true);
        Samet.SetActive(true);
        UFO.SetActive(true);

    }

    public void ReturnPlayerToEarth()
    {
        _animator.SetBool("faded", false);
        player.transform.position = preTeleportPosition;
        hasTeleported = false;

        // hide galaxy
        foreach (Transform tf in galaxyGroup.transform)
        {
            tf.gameObject.layer = 15;
        }

        teleportParticle.SetActive(false);
        Samet.SetActive(false);
        UFO.SetActive(false);
    }
}
