using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class InteractionComponent : MonoBehaviour
{
    protected CapsuleCollider interactionCollider;

    readonly private Collider[] _LatestInteractionColliders = new Collider[5];

    readonly private List<Collider> _ActiveInteractionColliders = new List<Collider>();

    public UnityEvent<InteractContext> OnInteractContextUpdated;

    // Start is called before the first frame update
    void OnEnable()
    {
        interactionCollider = GetComponent<CapsuleCollider>();
        if (ReferenceEquals(interactionCollider, null))
        {
            Debug.LogError("Interaction Component Owner has no Collider");
            return;
        }
    }

    public void OnInteract()
    {
        Debug.Log("Interacting");
        DistanceBasedInteraction(_ActiveInteractionColliders.ToArray());

    }

    // Common entry point, for both overlap and active collider based interactions
    private void DistanceBasedInteraction(Collider[] colliders)
    {

        if (colliders.Length == 0)
        {
            Debug.LogWarning("No Colliders provided to Interaction function. Cannot proceed");
            return;
        }

        float BestDistance = float.MaxValue;
        int BestIndex = -1;
        for (int i = 0; i < colliders.Length; i++)
        {
            // If the Collider is still valid
            if (!ReferenceEquals(colliders[i], null))
            {
                if (ReferenceEquals(colliders[i].gameObject, gameObject))
                {
                    continue;
                }


                // If this is the first index or if the Distance between the interactable and the owning object is 
                // smaller than the current best distance
                float CurrDistance = Vector3.Distance(
                        colliders[i].transform.position,
                        transform.position);
                if (i == 0 || CurrDistance < BestDistance)
                {
                    BestIndex = i;
                    BestDistance = CurrDistance;
                }
            }
        }

        // Only perform the GetComponent function when absolutely needed and cache the result
        IInteractableInterface interactable = colliders[BestIndex].gameObject.GetComponent<IInteractableInterface>();

        if (ReferenceEquals(interactable, null))
        {
            Debug.LogWarning($"Attempting to interact with {colliders[BestIndex].gameObject}. No Components implementing IInteractableInterface found, returning");
            return;
        }

        InteractContext interactContext = new InteractContext();
        interactContext.InitialiseContext();

        // Attempt the interaction
        if (interactable.IsInteractable(this))
        {
            interactable.AttemptInteraction(this, ref interactContext);
        }
        else
        {
            interactable.GetInteractContext(this, out interactContext);
        }

        if (interactContext.IsValidContext())
        {
            OnInteractContextUpdated.Invoke(interactContext);
        }

    }


    #region Active Collision Based Interaction


    private void OnTriggerEnter(Collider other)
    {
        
        if (ReferenceEquals(other.gameObject.GetComponent<IInteractableInterface>(), null))
        {
            Debug.LogWarning($"Attempting to add {other.gameObject} to Active Interaction Colliders. No Components implementing IInteractableInterface found, returning");
            return;
        }

        _ActiveInteractionColliders.Add(other);
        Debug.Log($"Successfully added {other.gameObject}");
        
    }

    private void OnTriggerExit(Collider other)
    {
        _ActiveInteractionColliders.Remove(other);
        Debug.Log($"Successfully Removed {other.gameObject}");
    }


    #endregion


}
