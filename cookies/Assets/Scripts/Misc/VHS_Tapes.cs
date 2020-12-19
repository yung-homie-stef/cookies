using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VHS_Tapes : MonoBehaviour
{
    public int threadIndex;
    public GameObject tapes;
    public string tapeDesc;
    public string author;
    public Text tapeDescText;
    public Text authorText;
    public bool _hasUnlocked = false;

    private Tape_List _tapeList;
    

    // Start is called before the first frame update
    void Start()
    {
        _tapeList = tapes.GetComponent<Tape_List>();
    }

    public void DisplayVHSTape()
    {
        if (_hasUnlocked)
        {
            tapeDescText.text = tapeDesc;
            authorText.text = author;
            _tapeList.EnableVHSTapes(threadIndex);
        }
    }

}
