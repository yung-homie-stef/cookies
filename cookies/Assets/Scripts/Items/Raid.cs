using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raid : Action
{
    public int requestedIndex;

    private int dex;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Use(int itemIndex)
    {
        StartCoroutine(TripOut(5.0f));
        dex = itemIndex;
    }

    private IEnumerator TripOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(requestedIndex);
        Destroy(_inventory.playerInventoryItems[dex]);
        Inventory.instance.RemoveItem(Inventory.instance.items[dex]);
    }
}
