using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Manager : MonoBehaviour
{
    public GameObject glock;
    public GameObject pamphlet; // Son of Sal Thread
    public GameObject altarDoor;

    private ProgressInformation _playersTotalProgress;

    // Start is called before the first frame update
    void Start()
    {
        _playersTotalProgress = Game_Manager.globalGameManager.GetProgressInformation();

        if (_playersTotalProgress.completedPaths[3])
        {
            glock.SetActive(true);
            pamphlet.SetActive(true);
            UnlockDoors(altarDoor);
        }

    }

    void UnlockDoors(GameObject door)
    {
        if (door.GetComponent<OpenableInteractable>())
        {
            door.GetComponent<OpenableInteractable>().isLocked = false;
        }
    }


}
