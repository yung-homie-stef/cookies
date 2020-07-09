using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Level : MonoBehaviour
{
    public GameObject blackout;
    public int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        blackout.GetComponent<Animator>().SetBool("faded", true);
        StartCoroutine(Transition(3.0f));
    }

    private IEnumerator Transition(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
