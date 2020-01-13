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
        StartCoroutine(LoadAsyncOperation());
    }

   private IEnumerator LoadAsyncOperation()
   {
        // create an async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);

        yield return new WaitForEndOfFrame();
   }
}
