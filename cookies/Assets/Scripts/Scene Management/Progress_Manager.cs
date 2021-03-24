using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Manager : MonoBehaviour
{

    public Player playerMans;

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
    public GameObject burnerPhone;
    [Space(10)]

    [Header("Crown Fried")]
    public Fast_Food_Worker cashier;
    public GameObject commercial;
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
            boxCoupon.SetActive(false);
            playerMans.gameObject.transform.position = newSpawn.position;
            UnlockDoors(altarDoor);
            introTriggers.SetActive(false);
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
            commercial.SetActive(true);
        }

        if (_playersTotalProgress.totalCompletedPaths >= 7) // unlock Glad Boys after completing 7 threads
        {
            UnlockDoors(normansDoor);
            UnlockDoors(manifestoDoor);
            OpenUnlockedDoors(normansDoor);
            OpenUnlockedDoors(manifestoDoor);       
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
        door.SetOpenToggle(0);
        door.SetCloseToggle(1);
        door.EnactOpening();
    }

    void EnableOrDisable(GameObject obj, bool act)
    {
        obj.SetActive(act);
    }


}
