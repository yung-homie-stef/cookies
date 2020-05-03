using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager globalAudioManager = null;

    public Sound[] soundArray;

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
         foreach (Sound s in soundArray) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.intangibleAudioSources;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        PlaySound("intro");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == name);
        s.source.Play();
        
    }

}
