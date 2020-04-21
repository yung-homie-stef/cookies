using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huxley_Thread_Trigger : MonoBehaviour
{
    public End_Condition heavens_front_porch_Thread;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            StartCoroutine(CompleteHuxleysThread(3.0f));
        }
    }

    private IEnumerator CompleteHuxleysThread(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(heavens_front_porch_Thread);
    }

}
