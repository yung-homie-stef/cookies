using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class FootstepsSoundControl : MonoBehaviour
{
    public AudioClip[] footStepSounds;
    public AudioSource playerAudioSource;

    public void Step()
    {
        int randomStepSound = Random.Range(0, footStepSounds.Length);
        PlayFootstep(randomStepSound);
    }

    private void PlayFootstep(int stepNum)
    {
        playerAudioSource.PlayOneShot(footStepSounds[stepNum]);
    }
}
