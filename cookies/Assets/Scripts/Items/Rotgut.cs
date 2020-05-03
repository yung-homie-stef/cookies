using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotgut : Action
{
    public Camera VHSCamera;
    public AudioClip drinkSound;

    private bool _isDrunk = false;
    private Drunk _drunkScript;

    void Start()
    {
        _inventory = player.GetComponent<Inventory>();
        _drunkScript = VHSCamera.gameObject.GetComponent<Drunk>();
    }

    public override void Use()
    {
        if (!_isDrunk)
        {
            GetComponent<AudioSource>().PlayOneShot(drinkSound);
            _drunkScript.enabled = true;
            StartCoroutine(SoberUp(20.0f));
            _isDrunk = true;
        }
    }

    private IEnumerator SoberUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _drunkScript.enabled = false;
        base.Use();
        Destroy(gameObject);
    }

}
