using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 2)]

public class Item : ScriptableObject
{
    public string itemName = "NAME";
    public string itemDesc = "DESC";
    public Sprite icon = null;
    public GameObject duplicate = null;

    public virtual void Use()
    {
        Debug.Log("using + " + itemName);
    }
}
