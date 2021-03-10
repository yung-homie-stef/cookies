using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public HealthBar[] bars;
    public Victim[] boss;

    private void OnTriggerEnter(Collider other)
    {
        for (int i =0; i < boss.Length; i++)
        {
            boss[i].enabled = true;

            if (boss[i] is Nazi)
            {
                bars[i].gameObject.SetActive(true);
                boss[i].GetComponent<Nazi>().BeginBattle();
            }
            if (boss[i] is Norman)
            {
                bars[i].gameObject.SetActive(true);
                boss[i].GetComponent<Norman>().BeginBattle();
            }
            if (boss[i] is Ichi)
            {
                bars[i].gameObject.SetActive(true);
                boss[i].GetComponent<Ichi>().BeginBattle();
            }
            if (boss[i] is Louis_Ray)
            {
                bars[i].gameObject.SetActive(true);
                boss[i].GetComponent<Louis_Ray>().BeginBattle();
            }
            if (boss[i] is Eddie)
            {
                bars[i].gameObject.SetActive(true);
                boss[i].GetComponent<Eddie>().BeginBattle();
            }
        }
    }
}
