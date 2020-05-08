using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preface : MonoBehaviour
{
    public GameObject blackOut;


    private void Start()
    {
        Cursor.visible = true;
    }

    public void FadePrefaceScreen()
    {
        blackOut.GetComponent<Animator>().SetBool("faded", true);
        StartCoroutine(GoToStartMenu(3.0f));
    }

    private IEnumerator GoToStartMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
