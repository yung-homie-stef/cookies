using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // start async operation
        StartCoroutine(LoadAsyncOperation(3.0f));
    }

   private IEnumerator LoadAsyncOperation(float waitTime)
   {
        yield return new WaitForSeconds(waitTime);
        // create an async operation

        if (Game_Manager.globalGameManager.GetProgressInformation().totalCompletedPaths == 0)
        {
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
            Audio_Manager.globalAudioManager.PlaySound("ambiance", Audio_Manager.globalAudioManager.musicSoundArray);
        }
        else
        {
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(3);
            Audio_Manager.globalAudioManager.PlaySound("ambiance", Audio_Manager.globalAudioManager.musicSoundArray);
        }
        
   }
}
