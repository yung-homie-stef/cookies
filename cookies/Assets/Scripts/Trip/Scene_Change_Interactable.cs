using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change_Interactable : Interactable
{
    public int requestedIndex;

    public override void InteractAction()
    {
        SceneManager.LoadScene(requestedIndex);
    }
}
