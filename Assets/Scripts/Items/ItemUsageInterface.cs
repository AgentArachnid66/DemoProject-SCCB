using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemUsageInterface
{
    public string Name { get; }
    public string Description { get; }

    public bool OnUse(InteractionComponent interactionComponent);
}
