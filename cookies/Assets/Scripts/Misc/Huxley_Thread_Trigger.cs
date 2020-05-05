using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huxley_Thread_Trigger : MonoBehaviour
{
    public End_Condition heavens_front_porch_Thread;
    public GameObject blackOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            StartCoroutine(CompleteHuxleysThread(5.0f));
            blackOut.GetComponent<Animator>().SetBool("faded", true);
        }
    }

    private IEnumerator CompleteHuxleysThread(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(heavens_front_porch_Thread);
    }

}
