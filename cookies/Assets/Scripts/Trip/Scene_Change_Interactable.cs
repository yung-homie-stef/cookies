using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change_Interactable : Interactable
{
    public int requestedIndex;
    public GameObject transitionScreen;

    public override void InteractAction()
    {
        transitionScreen.SetActive(true);
        StartCoroutine(ChangeScene(2.0f));
    }

    private IEnumerator ChangeScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(requestedIndex);
    }
}
