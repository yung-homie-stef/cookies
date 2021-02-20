using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartel_Member : MonoBehaviour
{
    public static int membersAlive = 3;
    private bool _dead = false;
    
    public void ReduceMemberNumber()
    {
        if (!_dead)
        {
            membersAlive--;
            _dead = true;
            Debug.Log(membersAlive);
        }

        if (membersAlive == 0)
        {
            // complete thread
        }
    }
    
}
