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
    public string[] pathNames;
    
    public ProgressInformation()
    {
        completedPaths = new bool[12];
        pathNames = new string[12];

        for (int i = 0; i < pathNames.Length; i++)
        {
            pathNames[i] = "--:--"; 
        }

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
    public bool VHSEffectOn = true;
    public GameObject mainMenu;
    public GameObject endScreen;
    public GameObject settingsScreen;
    public GameObject controlsScreen;
    public GameObject[] _endGameModels;
    private int _lastCompletedPath = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (globalGameManager)
        {
            Debug.Log("beans");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // when it spawns, once get deleted upon changing scenes
        globalGameManager = this;

        End_Menu em;
        if (em = endScreen.GetComponent<End_Menu>())
        {
            em.Init();
        }


        playerProgress = new ProgressInformation();
        playerProgress = Save_Load_Test.LoadProgress();
        mainMenuScript = mainMenu.GetComponent<MainMenu>();
        endScreen.SetActive(false);
        settingsScreen.SetActive(false);
        controlsScreen.SetActive(false);
        UpdateThreadTitles();
        Debug.Log(Application.persistentDataPath);
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

    public void UpdateThreadTitles()
    {
        for (int i = 0; i < playerProgress.completedPaths.Length; i++)
        {
            
            {
                mainMenuScript.threadTitleTexts[i].text = playerProgress.pathNames[i];
            }
        }
    }

    public ProgressInformation GetProgressInformation()
    {
        return playerProgress; 
    }

    public void EndGame(End_Condition condition)
    {
        Cursor.visible = true;

        End_Menu.globalEndMenu.SetStatusOfThreadCompletion(condition);
        CompletePath(condition.threadID, condition.threadName);
    
        SceneManager.LoadScene(5);
             
    }

    public void OnEndScreenReached()
    {
        endScreen.SetActive(true);
        Instantiate(_endGameModels[_lastCompletedPath]);
    }

    void CompletePath(int pathID, string pathName)
    {
        if (!playerProgress.completedPaths[pathID])
        {
            Debug.Log("path " + pathID + " has been completed");
            playerProgress.totalCompletedPaths++;
            playerProgress.completedPaths[pathID] = true; // this path is now complete
            playerProgress.pathNames[pathID] = pathName;
            Save_Load_Test.SaveProgress(playerProgress);
            
        }

        _lastCompletedPath = pathID;
    }

}
