using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Mana Item", menuName = "ScriptableObjects/Items/Mana", order = 1)]
public class ManaItem : ItemData
{
    public override bool OnUse(InteractionComponent interactionComponent)
    {
        Debug.Log($"Using Mana Item");
        return base.OnUse(interactionComponent);
    }
}
