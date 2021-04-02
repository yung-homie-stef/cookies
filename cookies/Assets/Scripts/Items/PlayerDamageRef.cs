using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageRef : MonoBehaviour
{
    public Player playerScript;

    public int GetPlayerDamage()
    {
        return Player.meleeDamage;
    }
}
