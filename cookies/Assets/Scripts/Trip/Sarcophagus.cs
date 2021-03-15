using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarcophagus : MonoBehaviour
{
    public GameObject sarcophagus;

    private GameObject _sarcophagusDoor;

    private void Start()
    {
        _sarcophagusDoor = sarcophagus.transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        sarcophagus.tag = "Interactable";
        _sarcophagusDoor.GetComponent<Animator>().SetBool("is_opened", true);
        Audio_Manager.globalAudioManager.PlaySound("tape", Audio_Manager.globalAudioManager.intangibleSoundArray);
    }
}
