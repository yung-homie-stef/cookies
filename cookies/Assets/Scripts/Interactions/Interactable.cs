using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    public virtual void ConversationEndEvent()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // for when the player clicks upon them
    public abstract void Interact();
}
