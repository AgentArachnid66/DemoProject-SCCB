using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IInteractableInterface
{
    public bool AttemptInteraction(InteractionComponent interactionComponent, ref InteractContext interactContext)
    {
        Debug.Log("Attempting Interaction with Item");
       
        if(ReferenceEquals(ItemData, null))
        {
            interactContext.AddWarning("Item Data is Invalid");
            return false;
        }

        if(ReferenceEquals(interactionComponent, null))
        {
            interactContext.AddWarning("Interaction Component is Invalid");
            return false;
        }

        return ItemData.OnUse(interactionComponent);

    }

    public void GetInteractContext(InteractionComponent interactionComponent, out InteractContext interactContext)
    {
        interactContext = new InteractContext();
    }

    public bool IsInteractable(InteractionComponent interactionComponent)
    {
        return true;
    }

    [SerializeReference]
    protected ItemData ItemData;


}
