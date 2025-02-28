using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableInterface
{
    bool IsInteractable(InteractionComponent interactionComponent);
    bool AttemptInteraction(InteractionComponent interactionComponent);
}
