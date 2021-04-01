using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Sound_Effects : MonoBehaviour
{
    public static Melee_Sound_Effects globalMeleeSounds;
    public AudioClip[] doorSFX; // 0=open, 1=close, 3=locked, 4=unlock, 5=bust

    // Start is called before the first frame update
    void Start()
    {
        if (!globalMeleeSounds)
        {
            globalMeleeSounds = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
