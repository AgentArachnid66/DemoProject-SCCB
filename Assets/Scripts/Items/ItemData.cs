using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemData : ScriptableObject, IItemUsageInterface
{

    [SerializeField]
    protected string ItemName;

    [SerializeField]
    protected string ItemDescription;



    public string Name => ItemName;

    public string Description => ItemDescription;

    public virtual bool OnUse(InteractionComponent interactionComponent)
    {
        Debug.Log($"Using Item {this}");
        return true;
    }
}
