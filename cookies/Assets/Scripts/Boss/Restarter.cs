using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{

    public Victim[] boss;

    private void OnTriggerEnter(Collider other)
    {
        for (int i =0; i < boss.Length; i++)
        {
            boss[i].enabled = true;

            if (boss[i] is Nazi)
            {
                boss[i].GetComponent<Nazi>().BeginBattle();
            }
            if (boss[i] is Norman)
            {
                boss[i].GetComponent<Norman>().BeginBattle();
            }
            if (boss[i] is Nazi)
            {
                boss[i].GetComponent<Ichi>().BeginBattle();
            }
            if (boss[i] is Nazi)
            {
                boss[i].GetComponent<Louis_Ray>().BeginBattle();
            }
            if (boss[i] is Nazi)
            {
                boss[i].GetComponent<Eddie>().BeginBattle();
            }
        }
    }
}
