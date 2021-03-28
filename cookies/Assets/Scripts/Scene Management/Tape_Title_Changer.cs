using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tape_Title_Changer : MonoBehaviour
{
    public Button[] tapeTitleButtons = new Button[10];

    private ProgressInformation _playersTotalProgress;
    private Text[] childTextObjects = new Text[10];

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < 10; i++)
        {
            childTextObjects[i] = tapeTitleButtons[i].transform.GetChild(0).GetComponent<Text>();
        }

    }

    void CheckAndUpdateTapeTitles()
    {

    }
}
