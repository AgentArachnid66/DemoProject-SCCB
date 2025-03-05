using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Items/Equipment", order = 1)]

public class EquipmentItem : ItemData
{
    public override bool OnUse(InteractionComponent interactionComponent)
    { 
        Debug.Log($"Equipping Item: {Name}");
        return base.OnUse(interactionComponent);
    }
}
