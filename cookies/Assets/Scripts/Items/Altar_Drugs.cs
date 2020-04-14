using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar_Drugs : Action
{
    public GameObject galaxyGroup;
    public GameObject blackout;
    public Transform galaxyTransform;
    public GameObject teleportParticle;

    private Animator _animator;
    [SerializeField]
    private Vector3 preTeleportPosition;
    private bool hasTeleported;

    // Start is called before the first frame update
    void Start()
    {
        hasTeleported = false;
        _animator = blackout.GetComponent<Animator>();
    }

    public override void Use()
    {
        if (!hasTeleported)
        {
            StartCoroutine(DrugsKickIn(5.0f));
            _animator.SetBool("faded", true);
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
    }
}
