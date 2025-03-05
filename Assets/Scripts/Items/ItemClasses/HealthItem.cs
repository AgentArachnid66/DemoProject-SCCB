using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "ScriptableObjects/Items/Health", order = 1)]
public class HealthItem : ItemData
{
    public int HealthModifier;

    public override bool OnUse(InteractionComponent interactionComponent)
    {
        Debug.Log($"Modifying Health by {HealthModifier}");

        return base.OnUse(interactionComponent);

    }
}
