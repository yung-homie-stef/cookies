using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Animator _animator;

    public enum RequirementType
    {
        None,
        Single,
        List
        
    }

    public RequirementType reqType = RequirementType.None;
    public string[] requiredTags;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    public virtual void ConversationEndEvent()
    {

    }

    public virtual void FailMessage()
    {

    }
  

    // Update is called once per frame
    void Update()
    {
        
    }

    // for when the player clicks upon them
    public virtual void Interact(Player player)
    {
        switch (reqType)
        {
            case RequirementType.None:
                {
                    InteractAction();
                    break;
                }
            case RequirementType.Single:
                {
                    player.OpenInteractInventory(this);
                    break;
                }
            case RequirementType.List:
                {
                    player.OpenInteractInventory(this);
                    break;
                }
        }
    }


    public virtual void Interact(Player player, string[] tags)
    {
        int tagIndex = -1;

        for (int i =0; i < requiredTags.Length && tagIndex == -1; i++)
        {
            for (int j = 0; j < tags.Length && tagIndex == -1; j++)
            {
                if (tags[j] == requiredTags[i])
                {
                    tagIndex = i;
                    break;
                }
            }
        }

        if (tagIndex == -1)
        {
            ItemUseFailed(player);
            return;
        }

        switch (reqType)
        {
            case RequirementType.Single:
                {
                    if (tagIndex != -1)
                    {
                        InteractAction();
                        reqType = RequirementType.None;
                        ItemUseSucceeded(player);
                    }
                    break;
                }
            case RequirementType.List:
                {
                    if (tagIndex != -1)
                    {
                        if (requiredTags.Length < 2)
                        {
                            InteractAction();
                            reqType = RequirementType.None; // this is when the player gives them the correct item
                            ItemUseSucceeded(player);
                        }
                        else
                        {
                            string[] tempArray = new string[requiredTags.Length - 1];

                            for (int i =0, j = 0; i < requiredTags.Length; i++, j+= ((i == tagIndex)?0:1))
                            {
                                if (i == tagIndex)
                                {
                                    continue;
                                }
                                tempArray[j] = requiredTags[i];
                            }
                            requiredTags = tempArray;
                        }
                    }
                    break;
                }
        }
        
    }

    public virtual void ItemUseFailed(Player player)
    {
        // TODO: display failed to use item message
        Debug.Log("whats wario doing on a bike?");
        player._inventory.DisplayFailedItemUsageText();
        player.CloseInteractInventory();
        Inventory.instance.inventoryUIScript.SetHeaderToBlank();
    }

    public virtual void ItemUseSucceeded(Player player)
    {
        player.DestroyUsedItem();
        player.CloseInteractInventory();
        Inventory.instance.inventoryUIScript.SetHeaderToBlank();
        Audio_Manager.globalAudioManager.PlaySound("ping", Audio_Manager.globalAudioManager.intangibleSoundArray);
    }

    public abstract void InteractAction();

}
