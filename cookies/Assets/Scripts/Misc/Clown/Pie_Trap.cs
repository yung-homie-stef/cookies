using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie_Trap : MonoBehaviour
{
    public GameObject pie;

    private Pie _pieScript;

    private void Start()
    {
        _pieScript = pie.GetComponent<Pie>();
    }

    public void EnableMechanismCollision()
    {
        _pieScript.enabled = true;
        pie.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableMechanismCollision()
    {
        _pieScript.enabled = false;
        _pieScript.hasAlreadySwung = true;
    }
}
