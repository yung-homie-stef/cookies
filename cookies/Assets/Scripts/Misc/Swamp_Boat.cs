using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Swamp_Boat : Interactable
{
    public GameObject actualBoatModel;
    public GameObject boatCollider;
    public Notice _notice;
    public End_Condition greenHellThread;
    public GameObject blackOut;

    public Text hintText;

    private GameObject player;
    private bool hasGas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasGas)
            {
                player = other.gameObject;
                player.transform.parent = actualBoatModel.transform;
            }
            else
            {
                // drive away
                player = other.gameObject;
                player.transform.parent = actualBoatModel.transform;
                blackOut.GetComponent<Animator>().SetBool("faded", true);
                gameObject.GetComponent<Animator>().SetBool("leaving", true);
                StartCoroutine(StartCredits(5.0f)) ;
            }

        }
    }

    public void UntetherPlayer()
    {
        boatCollider.SetActive(false);
        player.transform.parent = null;
        hintText.text += "\n- DMT cookin'";
    }

    public override void InteractAction()
    {
        if (!hasGas)
        {
            boatCollider.SetActive(false);
            hasGas = true;
        }
    }

    public override void FailMessage()
    {
        _notice.ChangeText("GASOLINE REQUIRED", 6.0f);
    }

    private IEnumerator CompleteSwampThread(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator StartCredits(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Audio_Manager.globalAudioManager.PlaySound("tape", Audio_Manager.globalAudioManager.intangibleSoundArray);
        SceneManager.LoadScene(9);

    }
}
