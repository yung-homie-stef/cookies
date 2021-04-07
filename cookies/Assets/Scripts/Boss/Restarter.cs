using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public HealthBar[] bars;
    public Victim[] boss;
    public string themeName;
    public int themeIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            for (int i = 0; i < boss.Length; i++)
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

            Audio_Manager.globalAudioManager.musicSoundArray[themeIndex].source.Stop();
            Audio_Manager.globalAudioManager.PlaySound(themeName, Audio_Manager.globalAudioManager.musicSoundArray);
            gameObject.SetActive(false);
        }
    }
}
