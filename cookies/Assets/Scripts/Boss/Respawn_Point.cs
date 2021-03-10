using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Point : MonoBehaviour
{
    public bool hasBoss;

    public Player playerScript;
    public Victim[] bossScript;
    public OpenableInteractable door;
    public HealthBar[] healthBar;
    public Animator[] animator;
    public Restarter bossRestarter;

    public int[] bossHP;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // set this transform as player's respawn point
        playerScript.respawnPoint = transform;
        playerScript.respawnScript = this;

        
    }

    public void ResetBossFight()
    {
        // unlock door
        // create trigger that activates fight
        // reset bosses health
        if (hasBoss)
        {
            door.isLocked = false;

            for (int i = 0; i < bossScript.Length; i++)
            {
                bossScript[i].hitPoints = bossScript[i].maxHitPoints;
                animator[i].enabled = false;

                healthBar[i].SetHealth(bossScript[i].maxHitPoints);
                healthBar[i].gameObject.SetActive(false);
                bossRestarter.gameObject.SetActive(true);
                bossScript[i].enabled = false;

                //TODO: activate healthbars
                // play boss theme
            }
        }
    }
}
