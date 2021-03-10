using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager globalAudioManager = null;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;

    public Sound[] intangibleSoundArray;
    public Sound[] musicSoundArray;

    void Awake()
    {

        if (!globalAudioManager)
        {
            globalAudioManager = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }

       
  
    }

    void Init()
    {
         foreach (Sound s in intangibleSoundArray) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = sfxMixer;
            
            s.source.clip = s.intangibleAudioSources;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        foreach (Sound s in musicSoundArray)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = musicMixer;

            s.source.clip = s.intangibleAudioSources;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        PlaySound("intro", musicSoundArray);
    }

    public void PlaySound(string name, Sound[] array)
    {
        Sound s = Array.Find(array, sound => sound.name == name);
        s.source.Play();
        
    }


}
