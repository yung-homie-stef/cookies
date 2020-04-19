using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/End_Condition", order = 0)]

public class End_Condition : ScriptableObject
{
    public string threadName;
    public string threadEffect;
    public string nextThreadHint;
    public int threadID;
    //public GameObject rotatingModel;

}
