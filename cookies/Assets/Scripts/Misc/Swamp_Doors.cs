using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swamp_Doors : Interactable
{
    public Animator pitchBlack;
    public int sceneIndex;

    public override void InteractAction()
    {
        pitchBlack.SetBool("faded", true);
        StartCoroutine(FadeBossMusic.StartFade(Audio_Manager.globalAudioManager.musicSoundArray[0].source, 5.0f, 0.0f));
        StartCoroutine(GoToSwamp(5.0f));
    }

    private IEnumerator GoToSwamp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
