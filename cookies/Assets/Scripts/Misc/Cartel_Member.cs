using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartel_Member : MonoBehaviour
{
    public static int membersAlive = 3;
    private bool _dead = false;
    public End_Condition pig_knuckles_Thread;

    public void ReduceMemberNumber()
    {
        if (!_dead)
        {
            membersAlive--;
            _dead = true;

            if (membersAlive == 0)
            {
                // complete thread
                StartCoroutine(CompleteCartelThread(5.0f));
            }
            Debug.Log(membersAlive);
        }

       
    }

    private IEnumerator CompleteCartelThread(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Game_Manager.globalGameManager.EndGame(pig_knuckles_Thread);

    }

}
