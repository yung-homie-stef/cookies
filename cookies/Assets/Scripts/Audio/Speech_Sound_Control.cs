using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Speech_Sound_Control : MonoBehaviour
{
    public AudioClip[] speechSounds;

    public void Speak()
    {
        int randomSpeechSound = Random.Range(0, speechSounds.Length);
        PlaySpeechSound(randomSpeechSound);
    }

    public void PlaySpeechSound(int speechNum)
    {
        GetComponent<AudioSource>().PlayOneShot(speechSounds[speechNum]);
    }
}
