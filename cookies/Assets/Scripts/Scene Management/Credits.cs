using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject noSignalScreen;
    public End_Condition green_hell_thread;

    public void FinishRolling()
    {
        noSignalScreen.SetActive(true);
        StartCoroutine(FinishGame(3.0f));
    }

    private IEnumerator FinishGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(green_hell_thread);

    }
}
