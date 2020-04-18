using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class ProgressInformation
{
    public bool[] completedPaths;
    public int totalCompletedPaths;
    public string newlyCompletedPath;
    
    public ProgressInformation()
    {
        completedPaths = new bool[12];

        for (int i = 0; i < completedPaths.Length; i++)
        {
            completedPaths[i] = false; // by default player has not completed any paths
        }
    }
}

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager globalGameManager = null;
    private ProgressInformation playerProgress = null;
    private MainMenu mainMenuScript;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerProgress = new ProgressInformation();
        playerProgress = Save_Load_Test.LoadProgress();
        mainMenuScript = mainMenu.GetComponent<MainMenu>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // when it spawns, once get deleted upon changing scenes
        globalGameManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < playerProgress.completedPaths.Length; i++)
            {
                Debug.Log(playerProgress.completedPaths[i]);
            }
            Debug.Log(playerProgress.totalCompletedPaths);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Save_Load_Test.SaveProgress(playerProgress);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            playerProgress = Save_Load_Test.LoadProgress();
        }


    }


    void CompletePath(int pathID, string pathName)
    {
        if (!playerProgress.completedPaths[pathID])
        {
            Debug.Log("path " + pathID + " has been completed");
            playerProgress.totalCompletedPaths++;
            playerProgress.completedPaths[pathID] = true; // this path is now complete
            mainMenuScript.threadTitleTexts[pathID].text = pathName;
            Save_Load_Test.SaveProgress(playerProgress);
            
        }
    }
}
