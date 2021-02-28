using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown_Thread_End : MonoBehaviour
{
    public End_Condition final_circus_Thread;
    public GameObject blackOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            blackOut.GetComponent<Animator>().SetBool("faded", true);
            StartCoroutine(CompleteClownThread(5.0f));
        }
    }

    private IEnumerator CompleteClownThread(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(final_circus_Thread);

    }
}
