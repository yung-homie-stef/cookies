using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raid : Action
{
    public int requestedIndex;
    public GameObject blackout;

    private int dex;

    public override void Use(int itemIndex)
    {
        blackout.GetComponent<Animator>().SetBool("faded", true);
        StartCoroutine(TripOut(10.0f));
        dex = itemIndex;
    }

    private IEnumerator TripOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Audio_Manager.globalAudioManager.musicSoundArray[0].source.Stop();
        Audio_Manager.globalAudioManager.PlaySound("tape", Audio_Manager.globalAudioManager.intangibleSoundArray);
        SceneManager.LoadScene(requestedIndex);
        Destroy(_inventory.playerInventoryItems[dex]);
        Inventory.instance.RemoveItem(Inventory.instance.items[dex]);
    }
}
