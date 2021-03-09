﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Manager : MonoBehaviour
{

    [Header("Heaven's Front Porch")]
    public GameObject glock;
    public GameObject pamphlet; 
    public OpenableInteractable altarDoor;
    [Space(10)]

    [Header("A Floridian Film")]
    public OpenableInteractable studioDoor;
    public OpenableInteractable drugDoor;
    [Space(10)]

    [Header("Pig Knuckles")]
    public OpenableInteractable rapperDoor;
    [Space(10)]

    [Header("Shinogi")]
    public GameObject blockade;
    [Space(10)]

    [Header("Black October")]
    public OpenableInteractable roachDoor;
    [Space(10)]

    [Header("The Green Hell")]
    public GameObject lobbyDoors;
    [Space(10)]

    [Header("Crown Fried")]
    public Fast_Food_Worker cashier;
    [Space(10)]

    [Header("Glad Boys")]
    public OpenableInteractable normansDoor;
    public OpenableInteractable manifestoDoor;
    [Space(10)]


    private ProgressInformation _playersTotalProgress;

    // Start is called before the first frame update
    void Start()
    {
        _playersTotalProgress = Game_Manager.globalGameManager.GetProgressInformation();

        if (_playersTotalProgress.completedPaths[0]) // completed Son of Sal Thread
        {
            glock.SetActive(true);
            pamphlet.SetActive(true); // unlocks Heaven's Front Porch
            UnlockDoors(altarDoor);
        }

        if (_playersTotalProgress.completedPaths[1]) // completed The Final Circus thread
        {
            UnlockDoors(studioDoor);
            UnlockDoors(rapperDoor); // unlocks Pig Knuckles and A Floridian Film
            UnlockDoors(drugDoor);
        }

        if (_playersTotalProgress.completedPaths[3])  // completed Heaven's Front Porch thread
        {
            EnableOrDisable(blockade, false); // Unlocks Shinogi
        }

        if (_playersTotalProgress.totalCompletedPaths >= 3)
        {
            UnlockDoors(roachDoor); // unlock Black October after completing 3 threads
        }

        if (_playersTotalProgress.totalCompletedPaths >= 5)
        {
            cashier.threadAvailable = true; // unlock Crown Fried after completing 5 threads
        }

        if (_playersTotalProgress.totalCompletedPaths >= 9)
        {
            // unlock The Green Hell after completing every other thread
            lobbyDoors.tag = "Interactable";
        }

    }

    void UnlockDoors(OpenableInteractable door)
    {
            door.isLocked = false;
    }

    void EnableOrDisable(GameObject obj, bool act)
    {
        obj.SetActive(act);
    }


}
