﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Manager : MonoBehaviour
{
    public Player playerMans;
    public Text hintText;

    [Header("Heaven's Front Porch")]
    public GameObject glock;
    public GameObject pamphlet; 
    public OpenableInteractable altarDoor;
    public GameObject boxCoupon;
    public Transform newSpawn;
    public GameObject introTriggers;
    [Space(10)]

    [Header("A Floridian Film")]
    public OpenableInteractable studioDoor;
    public OpenableInteractable drugDoor;
    public MeleeWeapon chainsaw;
    public GameObject unlockedItems;
    [Space(10)]

    [Header("Pig Knuckles")]
    public OpenableInteractable rapperDoor;
    [Space(10)]

    [Header("Shinogi")]
    public GameObject blockade;
    public GameObject ichisDoor;
    public GameObject killGraffiti;
    [Space(10)]

    [Header("Black October")]
    public OpenableInteractable roachDoor;
    public GameObject newNests;
    [Space(10)]

    [Header("The Green Hell")]
    public GameObject lobbyDoors;
    public GameObject burnerPhone;
    [Space(10)]

    [Header("Crown Fried")]
    public Fast_Food_Worker cashier;
    public GameObject commercial;
    [Space(10)]

    [Header("Glad Boys")]
    public OpenableInteractable normansDoor;
    public OpenableInteractable manifestoDoor;
    public OpenableInteractable sacrificeDoor;
    public GameObject bustedNormDoor;
    public GameObject bustedManifestoDoor;
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
            boxCoupon.SetActive(false);
            playerMans.gameObject.transform.position = newSpawn.position;
            UnlockDoors(altarDoor);
            introTriggers.SetActive(false);
            hintText.text += "\n- Letter under door";
        }

        if (_playersTotalProgress.completedPaths[1]) // completed The Final Circus thread 
        {
            UnlockDoors(studioDoor);
            UnlockDoors(rapperDoor); // unlocks Pig Knuckles and A Floridian Film
            UnlockDoors(drugDoor);
            hintText.text += "\n- Bathroom door fixed";
        }

        if (_playersTotalProgress.completedPaths[3])  // completed Heaven's Front Porch thread
        {
            ichisDoor.tag = "Interactable";
            killGraffiti.SetActive(true);
            EnableOrDisable(blockade, false); // Unlocks Shinogi
            hintText.text += "\n- Room 303 garbage collection";

        }

        if (_playersTotalProgress.totalCompletedPaths >= 3)
        {
            UnlockDoors(roachDoor); // unlock Black October after completing 3 threads
            newNests.SetActive(true);
            hintText.text += "\n- Infestation";
        }

        if (_playersTotalProgress.totalCompletedPaths >= 5)
        {
            cashier.threadAvailable = true; // unlock Crown Fried after completing 5 threads
            commercial.SetActive(true);
            hintText.text += "\n- $19.99 Family Cluck Bucket";
        }

        if (_playersTotalProgress.completedPaths[8]) // unlock chainsaw after completing Crown Fried
        {
            chainsaw.gameObject.SetActive(true);
        }

        if (_playersTotalProgress.completedPaths[2]) // unlock items after completing A Flordian Film
        {
            unlockedItems.gameObject.SetActive(true);
        }

        if (_playersTotalProgress.totalCompletedPaths >= 7) // unlock Glad Boys after completing 7 threads
        {
            UnlockDoors(sacrificeDoor);
            normansDoor.gameObject.SetActive(false);
            manifestoDoor.gameObject.SetActive(false);
            bustedManifestoDoor.SetActive(true);
            bustedNormDoor.SetActive(true);
            hintText.text += "\n- Mediocre male anger";
        }

        if (_playersTotalProgress.totalCompletedPaths >= 9)
        {
            // unlock The Green Hell after completing every other thread
            lobbyDoors.tag = "Interactable";
            burnerPhone.SetActive(true);
        }

    }

    void UnlockDoors(OpenableInteractable door)
    {
            door.isLocked = false;
    }

    void OpenUnlockedDoors(OpenableInteractable door)
    {
        door.SetOpenToggle(1);
        door.SetCloseToggle(0);
        door.isOpened = true;
    }

    void EnableOrDisable(GameObject obj, bool act)
    {
        obj.SetActive(act);
    }


}
